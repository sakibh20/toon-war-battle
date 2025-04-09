using UnityEngine;

namespace skb_sec._Project.Scripts.Hireables
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Hireable/Create New Worker Level", fileName = "Worker Level 1")]
    public class WorkerLevelDataSO : ScriptableObject
    {
        public int maxCapacity;
        public int upgradeCost;
    }
}
