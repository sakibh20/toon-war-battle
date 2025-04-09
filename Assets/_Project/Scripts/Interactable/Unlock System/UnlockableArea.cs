using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class UnlockableArea : Unlockables
    {
        [SerializeField] private GameObject colliders;
        [SerializeField] private Collider floorCollider;
        protected override void StartUnlockedInteraction()
        {
            
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
            newObjectCollider = floorCollider;
        }
    }
}
