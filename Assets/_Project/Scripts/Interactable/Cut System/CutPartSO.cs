using skb_sec._Project.Core.Pool_Manager.Script;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Cut_System
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Interactable/Parts/CutParts", fileName = "NewCutParts")]
    public class CutPartSO : ScriptableObject
    {
        public PoolItemType poolItemType;
    }
}
