using skb_sec._Project.Scripts.Tools;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable
{
    public interface IInteractable
    {
        public void StartInteraction();
        public void StopInteraction();
        public Transform ReturnTransform();
        public CuttingToolSO GetInteractionTool();
    }
}
