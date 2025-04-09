using System;
using System.Collections.Generic;
using skb_sec._Project.Scripts.Barrack.Soldier;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Damageable
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Interactable/DamageableManager", fileName = "DamageableManagerSO-")]
    public class DamageableManagerSO : ScriptableObject
    {
        public List<Damageable> allDamageableByEnemy = new List<Damageable>();

        

        public event Action OnBaseClear;
        public event Action OnDamageableRemoved;
        
        public void Enlist(Damageable damageable)
        {
            if (!allDamageableByEnemy.Contains(damageable))
            {
                allDamageableByEnemy.Add(damageable);
            }
        }

        public void DeList(Damageable damageable)
        {
            if (allDamageableByEnemy.Contains(damageable))
            {
                allDamageableByEnemy.Remove(damageable);

                if (allDamageableByEnemy.Count <= 0)
                {
                    FireOnBaseClear();
                }
                else
                {
                    FireOnDamageableRemoved();
                }
            }
        }

        private void FireOnBaseClear()
        {
            OnBaseClear?.Invoke();
        }      
        
        private void FireOnDamageableRemoved()
        {
            OnDamageableRemoved?.Invoke();
        }

        public void ClearList()
        {
            allDamageableByEnemy.Clear();
        }
    }
}
