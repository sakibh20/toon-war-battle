using UnityEngine;

namespace skb_sec._Project.Scripts.Weapon
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Weapon/Create New Weapon", fileName = "Weapon-")]
    public class WeaponSO : ScriptableObject
    {
        public Weapon weapon;
    }
}
