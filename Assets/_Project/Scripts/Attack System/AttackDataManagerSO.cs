using System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Attack_System
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Attack/Create Attack Data Manager", fileName = "AttackDataManagerSO")]
    public class AttackDataManagerSO : ScriptableObject
    {
        public bool playerAttacking;
        public bool playerUnderAttack;

        public event Action OnPlayerAttacking;
        public event Action OnPlayerUnderAttack;
        
        public event Action OnPlayerAttackComplete;
        public event Action OnPlayerUnderAttackComplete;
        

        public void FireOnPlayerAttacking()
        {
            playerAttacking = true;
            OnPlayerAttacking?.Invoke();
        }
        
        public void FireOnPlayerUnderAttack()
        {
            playerUnderAttack = true;
            OnPlayerUnderAttack?.Invoke();
        }
        
        public void FireOnPlayerAttackComplete()
        {
            playerAttacking = false;
            OnPlayerAttackComplete?.Invoke();
        }
        
        public void FirePlayerUnderAttackComplete()
        {
            playerUnderAttack = false;
            OnPlayerUnderAttackComplete?.Invoke();
        }
    }
}
