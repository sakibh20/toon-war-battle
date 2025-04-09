using System;
using System.Collections.Generic;
using _Project.Core.Custom_Debug_Log.Scripts;
using skb_sec._Project.Scripts.Storage;
using UnityEngine;

namespace skb_sec._Project.Scripts.Economy
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Resources/Resource Manager", fileName = "Resource Manager")]
    public class ResourceManagerSO : ScriptableObject
    {
        public List<ResourceSO> allResourceSo;
        [SerializeField] private StorageUpgradeLevelsSO bagLevelData;

        public event Action<int, ResourceSO> ResourceUpdated;
        public event Action StoreResource;
        public event Action ResourceLoaded;
        public event Action BagFull;
        public event Action BagNotFull;

        public bool WithinBagCapacity(int value)
        {
            var willBeCount = value;
            
            willBeCount += TotalResource();

            bool withinCapacity = willBeCount <= bagLevelData.allUpgrades[bagLevelData.CurrentLevel].maxCapacity;

            if (!withinCapacity)
            {
                FireBagFull();
            }
            else
            {
                FireBagNotFull();
            }

            return withinCapacity;
        }

        private void CheckBagCapacity()
        {
            bool withinCapacity = TotalResource() < bagLevelData.allUpgrades[bagLevelData.CurrentLevel].maxCapacity;

            if (!withinCapacity)
            {
                //CustomDebug.Log("FireBagFull");
                FireBagFull();
            }
            else
            {
                //CustomDebug.Log("FireBagNotFull");
                FireBagNotFull();
            }
        }

        public void IncreaseResource(int value, ResourceSO resourceSo, bool storeNow)
        {
            UpdateResource(value, resourceSo, storeNow);
        }

        public void DecreaseResource(int value, ResourceSO resourceSo, bool storeNow)
        {
            UpdateResource(-value, resourceSo, storeNow);
            
            CheckBagCapacity();
        }
        
        public void SetResource(int value, ResourceSO resourceSo, bool storeNow)
        {
            if (!allResourceSo.Contains(resourceSo))
            {
                CustomDebug.LogWarning("Given Resource is not in the introduced so list.");
                return;
            }
            
            resourceSo.Value = value;

            ResourceUpdated?.Invoke(resourceSo.Value, resourceSo);
            
            CheckBagCapacity();
            
            if(!storeNow) return;
            FireStoreResource();
        }
        
        private void UpdateResource(int value, ResourceSO resourceSo, bool storeNow)
        {
            if (!allResourceSo.Contains(resourceSo))
            {
                CustomDebug.LogWarning("Given Resource is not in the introduced so list.");
                return;
            }
            
            resourceSo.Value += value;
            
            ResourceUpdated?.Invoke(resourceSo.Value, resourceSo);

            if(!storeNow) return;
            FireStoreResource();
        }

        public int TotalResource()
        {
            int total = 0;

            foreach (ResourceSO resourceSo in allResourceSo)
            {
                total += resourceSo.Value;
            }

            return total;
        }

        private void FireBagFull()
        {
            BagFull?.Invoke();
        }
        
        private void FireBagNotFull()
        {
            BagNotFull?.Invoke();
        }
        
        public void FireStoreResource()
        {
            StoreResource?.Invoke();
        }

        public void OnResourceLoaded()
        {
            ResourceLoaded?.Invoke();
            
            CheckBagCapacity();
        }
    }
}
