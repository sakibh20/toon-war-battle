using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class UnlockableWoodCutter : Unlockables
    {
        //[SerializeField, Space] private GameObject halfBuildObject;
        [SerializeField] private GameObject woodCutterPlayer;

        [SerializeField] private Hireables.Hireables hireable;

        [SerializeField, Space] private UpgradeableWoodCutter upgradeableWoodCutter;
        
        protected override void StartUnlockedInteraction()
        {
            ShowInteractionUI();
        }

        protected override void StopUnlockedInteraction()
        {
            upgradeableWoodCutter.HideUpgradeButtonView();
        }

        protected override void HireWorkerOnUnlock()
        {
            hireable.Hire();
        }
        
        protected override void ShowInteractionUI()
        {
            upgradeableWoodCutter.ShowUpgradeButtonView();
        }

        protected override void OnUnlock()
        {
            //halfBuildObject.SetActive(false);
            woodCutterPlayer.SetActive(true);
        }
    }
}
