using System.Collections.Generic;
using skb_sec._Project.Scripts.Upgrade;
using UnityEngine;

namespace skb_sec._Project.Scripts.Storage
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Storage System/Create New Storage Upgrade Data", fileName = "New Storage UpgradeData")]
    public class StorageUpgradeLevelsSO : UpgradableSO
    {
        public ResourceCountWorldInfo worldInfoPrefab;
        public List<StorageLevelSO> allUpgrades = new List<StorageLevelSO>();
        
        // public int currentLevel;
        // public string title;
        // public Sprite storageSprite;

        public override void Distribute(AllStorageLevelData allUpgradableData)
        {
            for (int i = 0; i < allUpgradableData.allUpgradeLevelData.Count; i++)
            {
                if (allUpgradableData.allUpgradeLevelData[i].storageName == title)
                {
                    CurrentLevel = allUpgradableData.allUpgradeLevelData[i].currentLevel;
                    break;
                }
            }
        }

        public override void ClearData()
        {
            CurrentLevel = 0;
            
            for (int j = 0; j < allUpgrades.Count; j++)
            {
                for (int k = 0; k < allUpgrades[j].allIStorable.Count; k++)
                {
                    allUpgrades[j].allIStorable[k].storedValue = 0;
                }
            }
        }
    }
}
