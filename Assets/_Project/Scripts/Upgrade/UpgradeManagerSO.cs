using System;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Economy;
using UnityEngine;

namespace skb_sec._Project.Scripts.Upgrade
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Upgrade/Create Upgrade ManagerSO", fileName = "UpgradeManagerSO")]
    public class UpgradeManagerSO : ScriptableObject
    {
        [ReadOnly]public Upgradeable targetUpgradeable;
        [ReadOnly]public ResourceSO upgradeCostResource;
        [ReadOnly]public int upgradeCost;
        [ReadOnly]public int currentLevel;
        [ReadOnly]public int maxLevel;
        [ReadOnly]public string title;
        [ReadOnly]public string upgradeInfo;
        [ReadOnly]public Sprite storageIcon;

        public event Action ShowUpgradeView;

        public void FireShowUpgradeView()
        {
            ShowUpgradeView?.Invoke();
        }
    }
}
