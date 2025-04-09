using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Save_System.Scripts;
using skb_sec._Project.Scripts.Upgrade;
using UnityEngine;

namespace skb_sec._Project.Scripts.Storage
{
    public class UpgradableDataManager : DataManager.DataManager
    {
        [SerializeField] private UpgradeDataManagerSO upgradeDataManagerSo;

        private AllStorageLevelData _allUpgradeLevelData;

        protected override void Subscribe()
        {
            upgradeDataManagerSo.StorageDataUpdated += OnResourceUpdated;
        }

        protected override void UnSubscribe()
        {
            upgradeDataManagerSo.StorageDataUpdated -= OnResourceUpdated;
        }


        protected override void GetData()
        {
            var data = DataSaveManager.GetData(fileName);
            _allUpgradeLevelData = JsonUtility.FromJson<AllStorageLevelData>(data);

            DistributeData();
            
            upgradeDataManagerSo.FireStorageDataLoaded();
        }
        
        private void DistributeData()
        {
            if (_allUpgradeLevelData == null)
            {
                ClearData();
                return;
            }
            
            upgradeDataManagerSo.DistributeData(_allUpgradeLevelData);
            
            // for (int i = 0; i < _allUpgradeLevelData.allUpgradeLevelData.Count; i++)
            // {
            //     for (int j = 0; j < upgradeDataManagerSo.allStorageUpgradeLevels.Count; j++)
            //     {
            //         if (_allUpgradeLevelData.allUpgradeLevelData[i].storageName ==
            //             upgradeDataManagerSo.allStorageUpgradeLevels[j].title)
            //         {
            //             upgradeDataManagerSo.allStorageUpgradeLevels[j].currentLevel = _allUpgradeLevelData.allUpgradeLevelData[i].currentLevel;
            //             break;
            //         }
            //     }
            // }
        }

        protected override void GatherResourceData()
        {
            //_allUpgradeLevelData = new AllStorageLevelData();
            _allUpgradeLevelData = upgradeDataManagerSo.GatherData();

            // for (int i = 0; i < upgradeDataManagerSo.allStorageUpgradeLevels.Count; i++)
            // {
            //     LevelData data = new LevelData();
            //     
            //     data.storageName = upgradeDataManagerSo.allStorageUpgradeLevels[i].title;
            //     data.currentLevel = upgradeDataManagerSo.allStorageUpgradeLevels[i].currentLevel;
            //
            //     _allUpgradeLevelData.allUpgradeLevelData.Add(data);
            // }
        }

        protected override void SaveData()
        {
            string data = JsonUtility.ToJson(_allUpgradeLevelData);
            DataSaveManager.SaveData(data, fileName);
        }

        [Button]
        public override void ClearData()
        {
            // for (int i = 0; i < upgradeDataManagerSo.allStorageUpgradeLevels.Count; i++)
            // {
            //     upgradeDataManagerSo.allStorageUpgradeLevels[i].currentLevel = 0;
            //     
            //     for (int j = 0; j < upgradeDataManagerSo.allStorageUpgradeLevels[i].allUpgrades.Count; j++)
            //     {
            //         for (int k = 0; k < upgradeDataManagerSo.allStorageUpgradeLevels[i].allUpgrades[j].allIStorable.Count; k++)
            //         {
            //             upgradeDataManagerSo.allStorageUpgradeLevels[i].allUpgrades[j].allIStorable[k]
            //                 .storedValue = 0;
            //         }
            //     }
            // }
            
            upgradeDataManagerSo.ClearData();
            
            DataSaveManager.DeleteData(fileName);
        }
    }
    
    [Serializable]
    public class AllStorageLevelData
    {
        public List<LevelData> allUpgradeLevelData = new List<LevelData>();
    }
    

    [Serializable]
    public class LevelData
    {
        public string storageName;
        public int currentLevel;
    }
}
