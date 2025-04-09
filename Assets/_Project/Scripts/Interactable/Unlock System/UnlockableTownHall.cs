using skb_sec._Project.Scripts.Upgrade;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class UnlockableTownHall : Unlockables
    {
        [SerializeField] private Upgradeable upgradeable;

        protected override void StartUnlockedInteraction()
        {
            ShowInteractionUI();
        }

        protected override void StopUnlockedInteraction()
        {
            upgradeable.HideUpgradeButtonView();
        }

        protected override void HireWorkerOnUnlock()
        {
            
        }
        
        protected override void ShowInteractionUI()
        {
            upgradeable.ShowUpgradeButtonView();
        }

        protected override void OnUnlock()
        {
            upgradeable.ManageAppearance();
        }
    }
}
