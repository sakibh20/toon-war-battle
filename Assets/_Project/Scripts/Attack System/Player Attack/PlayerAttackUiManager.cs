using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Barrack.Soldier;
using skb_sec._Project.Scripts.Interactable.Damageable;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using UnityEngine;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Attack_System.Player_Attack
{
    public class PlayerAttackUiManager : MonoBehaviour
    {
        [SerializeField, Required] private DamageableManagerSO damageableManagerSo;
        [SerializeField, Required] private SoldierManagerSO soldierManager;
        [SerializeField, Required] private Button attackButton;

        [Space] [SerializeField, Required] private UnlockableManagerSO unlockableManagerSo;
        [SerializeField, Required] private UnlockableDataSO bridgeUnlockableDataSo;
        
        [SerializeField, Required] private AttackDataManagerSO attackDataManagerSo;

        private void Awake()
        {
            ManageAttackButtonInteraction();
            
            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            unlockableManagerSo.UnlockableLoaded += ManageAttackButtonVisibility;
            unlockableManagerSo.UnlockableUpdated += ManageAttackButtonVisibility;
            
            soldierManager.OnSoldierListUpdated += ManageAttackButtonInteraction;

            attackDataManagerSo.OnPlayerUnderAttack += HideAttackButton;
            attackDataManagerSo.OnPlayerAttacking += HideAttackButton;

            attackDataManagerSo.OnPlayerAttackComplete += ShowAttackButton;
            attackDataManagerSo.OnPlayerUnderAttackComplete += ShowAttackButton;
        }

        private void UnSubscribe()
        {
            unlockableManagerSo.UnlockableLoaded -= ManageAttackButtonVisibility;
            unlockableManagerSo.UnlockableUpdated -= ManageAttackButtonVisibility;
            
            soldierManager.OnSoldierListUpdated -= ManageAttackButtonInteraction;
            
            attackDataManagerSo.OnPlayerUnderAttack -= HideAttackButton;
            attackDataManagerSo.OnPlayerAttacking -= HideAttackButton;
            
            attackDataManagerSo.OnPlayerAttackComplete -= ShowAttackButton;
            attackDataManagerSo.OnPlayerUnderAttackComplete -= ShowAttackButton;
        }

        private void ManageAttackButtonInteraction()
        {
            attackButton.interactable = (soldierManager.SoldierCount > 0) &&(damageableManagerSo.allDamageableByEnemy.Count > 0);
        }
        

        private void ManageAttackButtonVisibility()
        {
            if (bridgeUnlockableDataSo.unlockableData.isUnlocked)
            {
                ShowAttackButton();
            }
            else
            {
                HideAttackButton();
            }
        }

        private void ShowAttackButton()
        {
            attackButton.gameObject.SetActive(true);
            ManageAttackButtonInteraction();
        }

        private void HideAttackButton()
        {
            attackButton.gameObject.SetActive(false);
        }
    }
}
