using UnityEngine;

namespace skb_sec._Project.Scripts.Town_Hall
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Upgrade/Town Hall/Create New Town Hall Level", fileName = "Town Hall Level -")]
    public class TownHallLevelSO : ScriptableObject
    {
        public int upgradeCost;
        public int maxPower;
    }
}
