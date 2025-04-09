using skb_sec._Project.Scripts.Upgrade;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class UnlockableBarrack : Unlockables
    {
        [Space]
        //[SerializeField] private Storage.Storage storage;
        [SerializeField] private Upgradeable upgradable;
        protected override void StartUnlockedInteraction()
        {
            //storage.StartWithdraw();
            ShowInteractionUI();
        }

        protected override void StopUnlockedInteraction()
        {
            //storage.StopWithdraw();
            upgradable.HideUpgradeButtonView();
        }

        protected override void ShowInteractionUI()
        {
            upgradable.ShowUpgradeButtonView();
        }

        protected override void HireWorkerOnUnlock()
        {
            
        }

        protected override void OnUnlock()
        {
            
        }
    }
}
