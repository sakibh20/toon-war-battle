using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using skb_sec._Project.Core.Pool_Manager.Script;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Hireables;
using skb_sec._Project.Scripts.Pick_System;
using skb_sec._Project.Scripts.Task_Manager;
using skb_sec._Project.Scripts.Tools;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Cut_System
{
    public abstract class Cutables : MonoBehaviour, IInteractable
    {
        [SerializeField, Space] private bool includeInHireableWorkList;
        
        [SerializeField] private CutablesInRangeSO cutablesInRangeSo;
        [SerializeField] private CutablesDataSO cutablesDataSo;
        [SerializeField] private CutPartSO cutPartSo;
        [SerializeField] private ResourceSO generatedResource;
        [SerializeField] private AlICutableSO alICutableSo;
        [SerializeField] private Transform treeTransform;
        
        
        public CuttingToolSO cuttingToolSo; 
        
        [SerializeField] protected List<GameObject> allParts;

        private Transform _transform;
        
        private int _currentHealth;
        private Collider _collider;

        public int CurrentHealth => _currentHealth;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _transform = transform;
            
            
            Initialize();
        }

        protected abstract void HitEffect();

        private void Initialize()
        {
            EnlistAsWorkable();
            
            SetInitialCurrentHealth();

            UpdateAppearance();
        }

        private void EnlistAsWorkable()
        {
            if(!includeInHireableWorkList) return;

            alICutableSo.Enlist(generatedResource, this);
        }
        
        [Button]
        private void RemoveFromList()
        {
            alICutableSo.RemoveFromList(generatedResource, this);
        }

        private void SetInitialCurrentHealth()
        {
            _currentHealth = allParts.Count - 1;
        }
        
        public void StartInteraction()
        {
            if (_currentHealth <= 0) return;
            
            _currentHealth -= 1;
            HitEffect();
            UpdateAppearance();
            GeneratePart();

            if (_currentHealth != 0) return;

            RemoveFromList();
            
            DisableCollider();
            StartCoroutine(RemoveFromCutableInRangeCo());
            Invoke(nameof(Respawn), cutablesDataSo.cutablesData.respawnTime);
        }

        public void StopInteraction()
        {
            
        }

        public Transform ReturnTransform()
        {
            return _transform;
        }

        public CuttingToolSO GetInteractionTool()
        {
            return cuttingToolSo;
        }

        private IEnumerator RemoveFromCutableInRangeCo()
        {
            yield return new WaitForEndOfFrame();
            cutablesInRangeSo.RemoveInteractableInRangeList(this);
            yield return null;
        }

        private void GeneratePart()
        {
            var item = PoolManager.instance.GetItemFromPool(cutPartSo.poolItemType).GetComponent<Pickable>();
            
            item.InitPickable(cutPartSo.poolItemType, _transform);
        }

        private void EnableCollider()
        {
            _collider.enabled = true;
        }

        private void DisableCollider()
        {
            _collider.enabled = false;
        }
        

        private void UpdateAppearance()
        {
            for (int i = 0; i < allParts.Count; i++)
            {
                allParts[i].SetActive(_currentHealth == i);
            }
        }
        
        private void Respawn()
        {
            Initialize();

            TweenRespawnSize();
        }
        
        private void TweenRespawnSize()
        {
            var targetScale = treeTransform.localScale;
            treeTransform.localScale = Vector3.zero;

            treeTransform.DOScale(targetScale, 0.8f).SetEase(Ease.InOutBack).OnComplete(OnCompleteAppear);
        }

        private void OnCompleteAppear()
        {
            EnableCollider();

            cutablesInRangeSo.FireScanInteractable();
        }
    }
}
