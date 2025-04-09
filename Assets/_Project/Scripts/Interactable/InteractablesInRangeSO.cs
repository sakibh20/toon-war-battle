using System;
using System.Collections.Generic;
using _Project.Core.Custom_Debug_Log.Scripts;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable
{
    public abstract class InteractablesInRangeSO : ScriptableObject
    {
        [ShowInInspector] protected List<IInteractable> allInteractableInRange = new List<IInteractable>();

        public event Action ScanInteractable;
        public event Action ListUpdated;
        
        public int AllInteractableCountInRange => allInteractableInRange.Count;

        public Transform GetFirstInteractable()
        {
            if (allInteractableInRange.Count > 0)
            {
                return allInteractableInRange[0].ReturnTransform();
            }
            return null;
        }

        public void AddInteractableInRangeList(IInteractable interactable)
        {
            if (!allInteractableInRange.Contains(interactable))
            {
                allInteractableInRange.Add(interactable);
            }
        }
        
        public void RemoveInteractableInRangeList(IInteractable interactable)
        {
            if (allInteractableInRange.Contains(interactable))
            {
                allInteractableInRange.Remove(interactable);

                FireListUpdated();
            }
        }
        
        public void StartInteraction()
        {
            for (int i = 0; i < allInteractableInRange.Count; i++)
            {
                allInteractableInRange[i].StartInteraction();
            }
        }


        public void StopInteraction()
        {
            for (int i = 0; i < allInteractableInRange.Count; i++)
            {
                allInteractableInRange[i].StopInteraction();
            }
        }
        
        public void ClearList()
        {
            allInteractableInRange.Clear();
        }

        protected void FireScan()
        {
            ScanInteractable?.Invoke();
        }      
        
        protected void FireListUpdated()
        {
            ListUpdated?.Invoke();
        }

        public abstract void FireScanInteractable();
    }
}
