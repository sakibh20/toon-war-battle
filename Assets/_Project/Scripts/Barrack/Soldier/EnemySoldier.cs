using UnityEngine;

namespace skb_sec._Project.Scripts.Barrack.Soldier
{
    public abstract class EnemySoldier : Soldier, IEnemy
    {
        public Transform GetTransform()
        {
            return transform;
        }
    }
}
