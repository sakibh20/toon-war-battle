using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Barrack.Soldier;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Interactable.Damageable;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace skb_sec._Project.Scripts.Attack_System.Enemy_Attack
{
    public class EnemyAttackManager : MonoBehaviour
    {
        [SerializeField, Required] private EnemyAttackUiManager enemyAttackManager;
        
        
        [SerializeField, Required] private NegotiateCostManagerSO negotiateCostManager;
        
        [SerializeField, Required] private ResourceSO costResource;
        [SerializeField, Required] private ResourceManagerSO resourceManagerSo;

        [SerializeField, Required] private SoldierManagerSO soldierManager;

        [SerializeField, Required] private DamageableManagerSO damageableManagerSo;
        
        [SerializeField, Required] private AttackDataManagerSO attackDataManagerSo;

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
                attackDataManagerSo.FirePlayerUnderAttackComplete();
            }
        } 
        
        private void OnDamageableRemoved()
        {
            if (damageableManagerSo.allDamageableByEnemy.Count <= 0)
            {
                attackDataManagerSo.FirePlayerUnderAttackComplete();
            }
        }

        [Button]
        public void Fight()
        {
            UnSubscribe();
            Subscribe();
            
            attackDataManagerSo.FireOnPlayerUnderAttack();
            //TODO: Stop Player Attack at this point
            
            enemyAttackManager.HideEnemyAttackingView();
            
            soldierManager.Attack(damageableManagerSo);
        }

        public void Negotiate()
        {
            enemyAttackManager.HideEnemyAttackingView();

            TweenTransfer();
        }
        
        private void TweenTransfer()
        {
            int value = costResource.Value;
            int endValue = costResource.Value - negotiateCostManager.negotiateCost;
            
            var tweenTime = 0.5f;
        
            DOTween.To(() => value, x => value = x, endValue, tweenTime).SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    resourceManagerSo.SetResource(value, costResource, false);
                }).
                OnComplete(OnCompleteTweenTransfer);
        }

        private void OnCompleteTweenTransfer()
        {
            resourceManagerSo.FireStoreResource();
        }

        private void OnDisable()
        {
            UnSubscribe();
            soldierManager.ClearList();
        }
    }
}
