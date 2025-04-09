using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Core.Custom_Debug_Log.Scripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Expense_Effect_System;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using skb_sec._Project.Scripts.Upgrade;
using TMPro;
using UnityEngine;

namespace skb_sec._Project.Scripts.Storage
{
    public abstract class Storage : MonoBehaviour
    {
        [SerializeField] private UpgradeDataManagerSO upgradeDataManagerSo;
        [SerializeField] protected StorageUpgradeLevelsSO allLevelsData;
        [SerializeField] protected ResourceManagerSO resourceManagerSo;
        [SerializeField] private UnlockableDataSO unlockableStorage;
        
        [SerializeField] private Transform infoParent;

        //private Coroutine _transferRoutine;
        
        private Tween _scoreTween;

        private List<ResourceCountWorldInfo> _allInfo;

        private void Awake()
        {
            _allInfo = new List<ResourceCountWorldInfo>();
        }
        
        protected virtual void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            upgradeDataManagerSo.StorageDataLoaded += GenerateStorageAllWorldInfo;
            
            unlockableStorage.Unlocked += GenerateStorageAllWorldInfo;
        }
        
        private void UnSubscribe()
        {
            upgradeDataManagerSo.StorageDataLoaded -= GenerateStorageAllWorldInfo;
            unlockableStorage.Unlocked -= GenerateStorageAllWorldInfo;
        }

        protected virtual void OnDisable()
        {
            UnSubscribe();
        }

        [Button]
        private void GenerateStorageAllWorldInfo()
        {
            if(!unlockableStorage.unlockableData.isUnlocked) return;
            
            ClearAll();
            
            _allInfo = new List<ResourceCountWorldInfo>();

            for (int i = 0; i < allLevelsData.allUpgrades[allLevelsData.CurrentLevel].allIStorable.Count; i++)
            {
                var info = Instantiate(allLevelsData.worldInfoPrefab, infoParent);
                info.InitStorageInfo(allLevelsData, i);
                
                _allInfo.Add(info);
            }
        }

        private void ClearAll()
        {
            for (int i = 0; i < _allInfo.Count; i++)
            {
                Destroy(_allInfo[i].gameObject);
            }
        }

        public void UpdateStorageWorldInfo()
        {
            for (int i = 0; i < _allInfo.Count; i++)
            {
                _allInfo[i].UpdateInfo();
            }
        }

        [Button]
        public void StoreOne(ResourceSO resourceSo)
        {
            for (int i = 0; i < allLevelsData.allUpgrades[allLevelsData.CurrentLevel].allIStorable.Count; i++)
            {
                if (allLevelsData.allUpgrades[allLevelsData.CurrentLevel].allIStorable[i].resource == resourceSo)
                {
                    allLevelsData.allUpgrades[allLevelsData.CurrentLevel].allIStorable[i].StoreValueOne();
                    UpdateStorageWorldInfo();
                }
            }
        }

        public bool Storable(ResourceSO resourceSo)
        {
            for (int i = 0; i < allLevelsData.allUpgrades[allLevelsData.CurrentLevel].allIStorable.Count; i++)
            {
                if (allLevelsData.allUpgrades[allLevelsData.CurrentLevel].allIStorable[i].resource == resourceSo)
                {
                    return allLevelsData.allUpgrades[allLevelsData.CurrentLevel].allIStorable[i].storedValue < allLevelsData.allUpgrades[allLevelsData.CurrentLevel].maxCapacity;
                }
            }
            
            return false;
        }

        public void StartWithdraw()
        {
            //CustomDebug.LogWarning("Handle Start Withdraw");
            InvokeRepeating(nameof(StartTransfer), 0.1f, 0.1f);
            //_transferRoutine = StartCoroutine(StarTransferCo());
        }

        private void StartTransfer()
        {
            if (!resourceManagerSo.WithinBagCapacity(1)) return;
                
            for (int i = 0; i < allLevelsData.allUpgrades[allLevelsData.CurrentLevel].allIStorable.Count; i++)
            {
                if (allLevelsData.allUpgrades[allLevelsData.CurrentLevel].allIStorable[i].storedValue > 0)
                {
                    allLevelsData.allUpgrades[allLevelsData.CurrentLevel].allIStorable[i].WithdrawOne(resourceManagerSo);
                    
                    ExpenseEffectManager.instance.Expense(allLevelsData.allUpgrades[allLevelsData.CurrentLevel].allIStorable[i].resource, 
                        transform.position, ReferenceManager.instance.player.position + Vector3.up);
                    
                    UpdateStorageWorldInfo();
                }
            }
        }

        public void StopWithdraw()
        {
            CancelInvoke(nameof(StartTransfer));
            //StopCoroutine(_transferRoutine);
        }
    }
}
