using UnityEngine;

namespace skb_sec._Project.Scripts.Character
{
    public class CharacterIdleState : CharacterStateBase
    {
        public CharacterIdleState(CharacterStateManager characterStateManager, CharacterStateFactory characterStateFactory) : base(characterStateManager, characterStateFactory)
        {
            IsRootState = true;
            //InitializeSubState();
        }

        public override void EnterState()
        {
            //CustomDebug.Log("EnterState CharacterIdleState");
            CharacterStateManager.cutablesInRangeSo.FireScanInteractable();
            
            CharacterStateManager.unlockablesInRangeSo.FireScanInteractable();
            CharacterStateManager.unlockablesInRangeSo.StartInteraction();

            CharacterStateManager.LookAtTarget();
            
            InitializeSubState();
        }

        protected override void UpdateState()
        {
            //CustomDebug.Log("CharacterIdleState");
            CheckSwitchStates();
        }

        protected override void ExitState()
        {
            CharacterStateManager.unlockablesInRangeSo.StopInteraction();
        }

        public override void CheckSwitchStates()
        {
            if (CharacterStateManager.controllerInputSo.inputInfo.speedModifier > 0)
            {
                SwitchStates(CharacterStateFactory.CharacterRun());
            }
        }

        public sealed override void InitializeSubState()
        {
            //CustomDebug.Log("InitializeSubState Idle");
            if (CharacterStateManager.cutablesInRangeSo.AllInteractableCountInRange > 0)
            {
                SetSubState(CharacterStateFactory.CharacterCuttingSubState());
            }
            else
            {
                SetSubState(CharacterStateFactory.CharacterIdleSubState());
            }
        }
    }
}
