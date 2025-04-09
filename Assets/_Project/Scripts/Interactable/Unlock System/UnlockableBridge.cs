using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Upgrade;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class UnlockableBridge : Unlockables
    {
        [SerializeField, Required] private GameObject colliders;
        protected override void StartUnlockedInteraction()
        {
            ShowInteractionUI();
        }

        protected override void StopUnlockedInteraction()
        {
            
        }

        protected override void HireWorkerOnUnlock()
        {
            
        }
        
        protected override void ShowInteractionUI()
        {
           
        }

        protected override void OnUnlock()
        {
            colliders.SetActive(false);
        }
    }
}
