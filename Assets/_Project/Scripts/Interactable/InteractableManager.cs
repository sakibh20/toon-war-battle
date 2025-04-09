using _Project.Core.Custom_Debug_Log.Scripts;
using skb_sec._Project.Scripts.Interactable.Cut_System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable
{
    public abstract class InteractableManager : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private InteractablesInRangeSO interactableInRangeSo;
        [SerializeField] private float scanRadius;

        [SerializeField] private int maxInteractable;

        private void Awake()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            interactableInRangeSo.ScanInteractable += ManageScan;
        }
        
        private void UnSubscribe()
        {
            interactableInRangeSo.ScanInteractable -= ManageScan;
        }
        
        
        private void ManageScan()
        {
            ClearList();
            ScanForInteractableInRange();
        }

        private void ClearList()
        {
            interactableInRangeSo.ClearList();
        }

        private void ScanForInteractableInRange()
        {
            var hitColliders = new Collider[maxInteractable];
            var numColliders = Physics.OverlapSphereNonAlloc(transform.position, scanRadius, hitColliders, targetLayer);
            

            for (int i = 0; i < numColliders; i++)
            {
                if (hitColliders[i].TryGetComponent(out IInteractable interactable))
                {
                    interactableInRangeSo.AddInteractableInRangeList(interactable);
                }
            }
        }

        private void OnDisable()
        {
            ClearList();
            UnSubscribe();
        }
    }
}
