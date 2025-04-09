using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using Doozy.Engine.UI;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Barrack.Soldier;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace skb_sec._Project.Scripts.Attack_System.Enemy_Attack
{
    public class EnemyAttackUiManager : MonoBehaviour
    {
        [SerializeField, Required] private UIView enemyAttackingView;
        [SerializeField, Required] private Button negotiateButton;
        
        [SerializeField, Required] private SoldierManagerSO soldierManager;
        [SerializeField, Required] private NegotiateCostManagerSO negotiateCostManager;
        
        [SerializeField, Required] private AttackDataManagerSO attackDataManagerSo;
        [SerializeField, Required] private ResourceSO costResource;
        
        
        [Space] 
        [SerializeField, Required] private UnlockableManagerSO unlockableManagerSo;
        [SerializeField, Required] private UnlockableDataSO bridgeUnlockableDataSo;

        private bool _bridgeUnlocked;


        [SerializeField, Range(10, 20)]private int maxAttackTimeAfterBridgeOpen;
        [SerializeField, Range(30, 50)]private int maxRandomAttackTime;

        private void Awake()
        {
            Subscribe();
        }
        
        private void Subscribe()
        {
            unlockableManagerSo.UnlockableLoaded += ManageEnemyAttack;
            unlockableManagerSo.UnlockableUpdated += ManageEnemyAttack;

            attackDataManagerSo.OnPlayerUnderAttackComplete += OnPlayerUnderAttackComplete;
        }

        private void UnSubscribe()
        {
            unlockableManagerSo.UnlockableLoaded -= ManageEnemyAttack;
            unlockableManagerSo.UnlockableUpdated -= ManageEnemyAttack;
            
            attackDataManagerSo.OnPlayerUnderAttackComplete -= OnPlayerUnderAttackComplete;
        }

        private void ManageEnemyAttack()
        {
            if(_bridgeUnlocked) return;
         
            _bridgeUnlocked = bridgeUnlockableDataSo.unlockableData.isUnlocked;
            
            if (_bridgeUnlocked)
            {
                var time = Random.Range(20, maxAttackTimeAfterBridgeOpen);
                Invoke(nameof(ShowEnemyAttackingView), time);
            }
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void OnPlayerUnderAttackComplete()
        {
            PrepareNextAttack();
        }


        [Button]
        public void ShowEnemyAttackingView()
        {
            if (attackDataManagerSo.playerAttacking)
            {
                CustomDebug.Log("attackDataManagerSo.playerAttacking. Returning");
                PrepareNextAttack();
                return;
            }
            
            if(soldierManager.SoldierCount <= 0)
            {
                PrepareNextAttack();
                CustomDebug.Log("Not enough soldier, Not Attacking");
                return;
            }
            
            CancelInvoke(nameof(ShowEnemyAttackingView));
            
            SetNegotiateButtonStatus();
            enemyAttackingView.Show();
        }

        
        public void HideEnemyAttackingView()
        {
            enemyAttackingView.Hide();
        }

        public void PrepareNextAttack()
        {
            var time = Random.Range(300, maxRandomAttackTime);
            
            CancelInvoke(nameof(ShowEnemyAttackingView));

            Invoke(nameof(ShowEnemyAttackingView), time);
        }

        private void SetNegotiateButtonStatus()
        {
            negotiateButton.interactable = costResource.Value >= negotiateCostManager.negotiateCost;
        }
    }
}
