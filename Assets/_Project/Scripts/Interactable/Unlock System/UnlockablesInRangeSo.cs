using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Interactable/Unlockables/UnlockablesInRange", fileName = "UnlockablesInRange")]
    public class UnlockablesInRangeSo : InteractablesInRangeSO
    {
        public override void FireScanInteractable()
        {
            FireScan();
        }
    }
}
