using skb_sec._Project.Scripts.Upgrade;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class UnlockableWatchTower : Unlockables
    {
        [SerializeField] private Upgradeable upgradable;
        protected override void StartUnlockedInteraction()
        {
            ShowInteractionUI();
        }

        protected override void StopUnlockedInteraction()
        {
            upgradable.HideUpgradeButtonView();
        }

        protected override void HireWorkerOnUnlock()
        {
            
        }
        
        protected override void ShowInteractionUI()
        {
            upgradable.ShowUpgradeButtonView();
        }

        protected override void OnUnlock()
        {
            
        }
    }
}
