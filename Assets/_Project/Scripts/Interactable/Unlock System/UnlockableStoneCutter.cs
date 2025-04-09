using _Project.Core.Custom_Debug_Log.Scripts;
using skb_sec._Project.Scripts.Upgrade;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class UnlockableStoneCutter : Unlockables
    {
        //[SerializeField, Space] private GameObject halfBuildObject;
        [SerializeField] private GameObject woodCutterPlayer;

        [SerializeField] private Hireables.Hireables hireable;

        [SerializeField, Space] private Upgradeable upgradeable;
        
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
            hireable.Hire();
        }
        
        protected override void ShowInteractionUI()
        {
            upgradeable.ShowUpgradeButtonView();
        }

        protected override void OnUnlock()
        {
            CustomDebug.Log("Stone Cutter On Unlock");
            //halfBuildObject.SetActive(false);
            Invoke(nameof(EnableWoodCutter), 1.0f);
        }

        private void EnableWoodCutter()
        {
            woodCutterPlayer.SetActive(true);
        }
    }
}
