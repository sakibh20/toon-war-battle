using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Tools;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Cut_System
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Interactable/Cutables/CutablesInRange", fileName = "CutablesInRange")]
    public class CutablesInRangeSO : InteractablesInRangeSO
    {

         public CuttingToolSO targetCuttingTool;
         
         public override void FireScanInteractable()
        {
            FireScan();
            
            DecideTargetCuttingTool();
        }
        
        private void DecideTargetCuttingTool()
        {
            if (AllInteractableCountInRange > 0)
            {
                targetCuttingTool = allInteractableInRange[0].GetInteractionTool();
            }
        }
    }
}
