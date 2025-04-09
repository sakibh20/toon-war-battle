using System;
using System.Collections.Generic;
using skb_sec._Project.Scripts.Storage;
using UnityEngine;

namespace skb_sec._Project.Scripts.Upgrade
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Storage System/Create Upgradable Data Manager", fileName = "Upgradable Data Manager SO")]
    public class UpgradeDataManagerSO : ScriptableObject
    {
        public List<UpgradableSO> allUpgradable;
        private AllStorageLevelData _allUpgradeLevelData;

        public event Action StorageDataUpdated;
        public event Action StorageDataLoaded;
        
        public void FireStorageDataUpdated()
        {
            StorageDataUpdated?.Invoke();
        }
        
        public void FireStorageDataLoaded()
        {
            StorageDataLoaded?.Invoke();
        }

        public void DistributeData(AllStorageLevelData allUpgradableData)
        {
            for (int i = 0; i < allUpgradable.Count; i++)
            {
                allUpgradable[i].Distribute(allUpgradableData);
            }
        }

        public AllStorageLevelData GatherData()
        {
            _allUpgradeLevelData = new AllStorageLevelData();
            
            for (int i = 0; i < allUpgradable.Count; i++)
            {
                LevelData data = new LevelData();
                
                data.storageName = allUpgradable[i].title;
                data.currentLevel = allUpgradable[i].CurrentLevel;

                _allUpgradeLevelData.allUpgradeLevelData.Add(data);
            }

            return _allUpgradeLevelData;
        }
        
        public void ClearData()
        {
            for (int i = 0; i < allUpgradable.Count; i++)
            {
                allUpgradable[i].ClearData();
            }
        }
    }
}
