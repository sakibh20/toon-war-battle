using System.Collections.Generic;
using UnityEngine;

namespace skb_sec._Project.Scripts.Hireables
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Hireable/Create Levels Holder", fileName = "All Levels For - ")]
    public class WorkerAllLevelsSO : ScriptableObject
    {
        public List<WorkerLevelDataSO> allUpgrades = new List<WorkerLevelDataSO>();

        public string title;
        public Sprite sprite;
        public int currentLevel;

        public bool hired;
        public int storedValue;
    }
}
