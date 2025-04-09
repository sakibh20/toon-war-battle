using skb_sec._Project.Scripts.Upgrade;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class UnlockableLumberCamp : Unlockables
    {
        [Space]
        [SerializeField] private Storage.Storage storage;
        [SerializeField] private Upgradeable upgradeableStorage;
        protected override void StartUnlockedInteraction()
        {
            storage.StartWithdraw();
            ShowInteractionUI();
        }

        protected override void StopUnlockedInteraction()
        {
            storage.StopWithdraw();
            upgradeableStorage.HideUpgradeButtonView();
        }

        protected override void HireWorkerOnUnlock()
        {
            
        }
        
        protected override void ShowInteractionUI()
        {
            upgradeableStorage.ShowUpgradeButtonView();
        }

        protected override void OnUnlock()
        {
            
        }
    }
}
