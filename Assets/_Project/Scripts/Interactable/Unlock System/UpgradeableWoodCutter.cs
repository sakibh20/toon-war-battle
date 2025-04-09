using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Hireables;
using skb_sec._Project.Scripts.Storage;
using skb_sec._Project.Scripts.Upgrade;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class UpgradeableWoodCutter : Upgradeable
    {
        [SerializeField, Space] private WorkerAllLevelsSO workerAllLevelsData;
        [SerializeField] private UpgradeManagerSO upgradeManagerSo;
        [SerializeField] private ResourceSO resourceSo;
        [SerializeField] private ResourceManagerSO resourceManagerSo;
        [SerializeField] private HireableUiManager hireableUiManager;
        
        protected override void Show()
        {
            SetValues();
            upgradeManagerSo.FireShowUpgradeView();
        }
        
        private void SetValues()
        {
            upgradeManagerSo.targetUpgradeable = this;
            upgradeManagerSo.upgradeCostResource = resourceSo;
            upgradeManagerSo.currentLevel = workerAllLevelsData.currentLevel;
            upgradeManagerSo.maxLevel = workerAllLevelsData.allUpgrades.Count-1;
            upgradeManagerSo.title = workerAllLevelsData.title.ToUpper();
            upgradeManagerSo.storageIcon = workerAllLevelsData.sprite;
            
            if (workerAllLevelsData.allUpgrades.Count-1 > workerAllLevelsData.currentLevel)
            {
                upgradeManagerSo.upgradeCost = workerAllLevelsData.allUpgrades[workerAllLevelsData.currentLevel+1].upgradeCost;

                upgradeManagerSo.upgradeInfo ="Capacity will be increased from " + workerAllLevelsData.allUpgrades[workerAllLevelsData.currentLevel].maxCapacity + " to "+
                                              workerAllLevelsData.allUpgrades[workerAllLevelsData.currentLevel+1].maxCapacity;
            }
        }

        protected override void ManageAppear()
        {
            
        }

        public override void Upgrade()
        {
            workerAllLevelsData.currentLevel += 1;

            int newVal = resourceSo.Value - workerAllLevelsData.allUpgrades[workerAllLevelsData.currentLevel].upgradeCost;
            resourceManagerSo.SetResource(newVal, resourceSo, true);
            hireableUiManager.UpdateInfo();

            SetValues();
        }
    }
}
