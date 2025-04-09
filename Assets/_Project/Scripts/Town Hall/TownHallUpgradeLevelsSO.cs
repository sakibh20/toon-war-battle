using System.Collections.Generic;
using skb_sec._Project.Scripts.Storage;
using skb_sec._Project.Scripts.Upgrade;
using UnityEngine;

namespace skb_sec._Project.Scripts.Town_Hall
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Upgrade/Town Hall/Town Hall All Upgrades", fileName = "Town Hall All Upgrade Data")]
    public class TownHallUpgradeLevelsSO : UpgradableSO
    {
        public ResourceCountWorldInfo worldInfoPrefab;
        public List<TownHallLevelSO> allUpgrades = new List<TownHallLevelSO>();

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
        }
    }
}
