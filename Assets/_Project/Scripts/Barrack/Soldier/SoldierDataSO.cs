using UnityEngine;

namespace skb_sec._Project.Scripts.Barrack.Soldier
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Soldier/SoldierDataSO", fileName = "SoldierDataSO-")]
    public class SoldierDataSO : ScriptableObject
    {
        public int damagePower;
        public int health;
        public float runSpeed;
        public float walkSpeed;
    }
}
