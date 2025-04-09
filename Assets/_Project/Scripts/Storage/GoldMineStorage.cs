using _Project.Core.Custom_Debug_Log.Scripts;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Expense_Effect_System;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Storage
{
    public class GoldMineStorage : Storage
    {
        [SerializeField, Required] private ResourceSO storingResourceSo;
        [SerializeField, Required] private UnlockableDataSO unlockableDataSo;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (unlockableDataSo.unlockableData.isUnlocked)
            {
                StartStoreInStorage();
            }
            else
            {
                unlockableDataSo.Unlocked += StartStoreInStorage;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            unlockableDataSo.Unlocked -= StartStoreInStorage;
        }


        [Button]
        public void StartStoreInStorage()
        {
            if (!unlockableDataSo.unlockableData.isUnlocked)
            {
                return;
            }
            
            if (Storable(storingResourceSo))
            {
                CancelInvoke(nameof(StartStoreInStorage));
                InvokeRepeating(nameof(StoreRepeating), 0.1f, 5f);
            }
            else
            {
                Invoke(nameof(StartStoreInStorage), 5.0f);
            }
        }
        
        
        private void StoreRepeating()
        {
            if (Storable(storingResourceSo))
            {
                StoreOne(storingResourceSo);
                resourceManagerSo.FireStoreResource();
            }
            else
            {
                StopStoring();
            }
        }
        
        private void StopStoring()
        {
            CancelInvoke(nameof(StoreRepeating));

            Invoke(nameof(StartStoreInStorage), 5.0f);
        }
    }
}
