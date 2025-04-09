using _Project.Core.Custom_Debug_Log.Scripts;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Barrack;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Storage;
using UnityEngine;

namespace skb_sec._Project.Scripts.Upgrade
{
    public class UpgradeableBag : Upgradeable
    {
        [SerializeField, Space] private StorageUpgradeLevelsSO allLevelsData;
        [SerializeField] private UpgradeManagerSO upgradeManagerSo;
        [SerializeField] private ResourceSO resourceSo;
        [SerializeField] private ResourceManagerSO resourceManagerSo;

        protected override void Show()
        {
            SetValues();
            upgradeManagerSo.FireShowUpgradeView();
        }

        private void SetValues()
        {
            upgradeManagerSo.targetUpgradeable = this;
            upgradeManagerSo.upgradeCostResource = resourceSo;
            upgradeManagerSo.currentLevel = allLevelsData.CurrentLevel;
            upgradeManagerSo.maxLevel = allLevelsData.allUpgrades.Count-1;
            upgradeManagerSo.title = allLevelsData.title.ToUpper();
            upgradeManagerSo.storageIcon = allLevelsData.sprite;
            
            if (allLevelsData.allUpgrades.Count-1 > allLevelsData.CurrentLevel)
            {
                upgradeManagerSo.upgradeCost = allLevelsData.allUpgrades[allLevelsData.CurrentLevel+1].upgradeCost;

                upgradeManagerSo.upgradeInfo ="Capacity will be increased from " + allLevelsData.allUpgrades[allLevelsData.CurrentLevel].maxCapacity + " to "+
                                              allLevelsData.allUpgrades[allLevelsData.CurrentLevel+1].maxCapacity;
            }
        }

        protected override void ManageAppear()
        {
            
        }

        public override void Upgrade()
        {
            //CustomDebug.Log("Handle Upgrade");
            allLevelsData.CurrentLevel = allLevelsData.CurrentLevel + 1;

            int newVal = resourceSo.Value - allLevelsData.allUpgrades[allLevelsData.CurrentLevel].upgradeCost;
            resourceManagerSo.SetResource(newVal, resourceSo, true);

            SetValues();
        }
    }
}
