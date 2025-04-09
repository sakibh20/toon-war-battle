using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using skb_sec._Project.Core.Pool_Manager.Script;
using skb_sec._Project.Scripts.Economy;
using UnityEngine;

namespace skb_sec._Project.Scripts.Pick_System
{
    public abstract class Pickable : MonoBehaviour, IPoolItem
    {
        [SerializeField] private ResourceSO resourceSo;
        [SerializeField] private ResourceManagerSO resourceManagerSo;
        [SerializeField] private int value;

        [SerializeField] private float autoPutBackTime;

        [SerializeField] private bool notPickableByHired;
        public bool destroyOnCollect;

        public bool NotPickableByHired => notPickableByHired;

        private Collider _collider;

        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
        
        private bool _picked;
        
        [SerializeField] private PoolItemType itemType;
        protected Transform objectTransform;
        protected readonly float maxDisplacement = 2.0f;

        private Vector3 _targetPos;
        private bool _addToResource;
        
        private ReferenceManager _referenceManager;

        protected abstract void PickUpEffect(Transform targetTransform);

        public bool DoesResourceMatch(ResourceSO matchWithResource)
        {
            return resourceSo == matchWithResource;
        }

        [Button]
        public void PickUp(Transform targetTransform, bool addToResource)
        {
            if(_picked) return;
            
            if(!resourceManagerSo.WithinBagCapacity(value)) return;
            
            _addToResource = addToResource;

            _picked = true;
            PickUpEffect(targetTransform);
        }

        protected void OnCompleteCollect()
        {
            if (_addToResource)
            {
                AddResource();   
            }
            PutBack();
        }

        private void AddResource()
        {
            resourceManagerSo.IncreaseResource(value, resourceSo, true);
        }

        public void InitPickable(PoolItemType poolItemType, Transform targetTransform)
        {
            _picked = false;
            itemType = poolItemType;
            
            objectTransform = transform;

            DisableCollider();
            
            objectTransform.localScale = Vector3.one;
            
            objectTransform.SetParent(targetTransform);
            
            //_collider.enabled = true;

            Vector3 targetPos = Vector3.up * 1f;

            objectTransform.localPosition = targetPos;

            Invoke(nameof(PutBack), autoPutBackTime);

            Appear();
        }

        public void InitExpenseable(PoolItemType poolItemType, Vector3 startPos, Vector3 targetPos)
        {
            // if (!_referenceManager)
            // {
            //     _referenceManager = ReferenceManager.instance;
            // }
            
            DisableCollider();
            
            itemType = poolItemType;
            _targetPos = targetPos;
            
            objectTransform = transform;
            objectTransform.localScale = Vector3.one;
            
            objectTransform.SetParent(null);
            
            objectTransform.transform.position = startPos;
            
            TweenPosition();
        }

        protected void EnableCollider()
        {
            if (!_collider)
            {
                _collider = GetComponent<Collider>();
            }

            _collider.enabled = true;
        }

        protected void DisableCollider()
        {
            if (!_collider)
            {
                _collider = GetComponent<Collider>();
            }

            _collider.enabled = false;
        }
        

        protected void EnablePickup()
        {
            EnableCollider();
        }

        private void TweenPosition()
        {
            objectTransform.DOJump(_targetPos, 3.2f,1,0.2f).OnComplete(PutBack);
        }

        protected abstract void Appear();

        public void PutBack()
        { 
            CancelInvoke(nameof(PutBack));

            if (destroyOnCollect)
            {
                Destroy(gameObject);
                return;
            }

            DisableCollider();

            objectTransform.DOKill();

            PoolManager.instance.PutBackItemInPool(itemType, gameObject);
        }
    }
}
