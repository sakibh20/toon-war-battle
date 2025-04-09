using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Barrack.Soldier;
using skb_sec._Project.Scripts.Interactable.Damageable;
using UnityEngine;

namespace skb_sec._Project.Scripts.Attack_System.Player_Attack
{
    public class PlayerAttackManager : MonoBehaviour
    {
        [SerializeField, Required] private SoldierManagerSO soldierManager;
        [SerializeField, Required] private DamageableManagerSO damageableManagerSo;
        
        [SerializeField, Required] private AttackDataManagerSO attackDataManagerSo;
        
        [Button]
        public void Attack()
        {
            UnSubscribe();
            Subscribe();
            
            soldierManager.Attack(damageableManagerSo);
            attackDataManagerSo.FireOnPlayerAttacking();
        }

        private void Subscribe()
        {
            soldierManager.OnSoldierListUpdated += OnSoldierListUpdated;
            damageableManagerSo.OnDamageableRemoved += OnDamageableRemoved;
        }

        private void UnSubscribe()
        {
            soldierManager.OnSoldierListUpdated -= OnSoldierListUpdated;
            damageableManagerSo.OnDamageableRemoved -= OnDamageableRemoved;
        }

        private void OnSoldierListUpdated()
        {
            if (soldierManager.SoldierCount <= 0)
            {
                attackDataManagerSo.FireOnPlayerAttackComplete();
            }
        } 
        
        private void OnDamageableRemoved()
        {
            if (damageableManagerSo.allDamageableByEnemy.Count <= 0)
            {
                attackDataManagerSo.FireOnPlayerAttackComplete();
            }
        }

        private void OnDisable()
        {
            UnSubscribe();
            
            soldierManager.ClearList();

            attackDataManagerSo.playerAttacking = false;
            attackDataManagerSo.playerUnderAttack = false;
        }
    }
}
