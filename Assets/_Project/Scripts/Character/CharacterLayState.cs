using UnityEngine;

namespace skb_sec._Project.Scripts.Character
{
    public class CharacterLayState : CharacterStateBase
    {
        public CharacterLayState(CharacterStateManager characterStateManager, CharacterStateFactory characterStateFactory) : base(characterStateManager, characterStateFactory)
        {
            IsRootState = true;
            //InitializeSubState();
        }

        public override void EnterState()
        {
            CharacterStateManager.characterAnimatorManager.LayAnimation();
        }

        protected override void UpdateState()
        {
        }

        protected override void ExitState()
        {
        }

        public override void CheckSwitchStates()
        {
        }

        public sealed override void InitializeSubState()
        {
            
        }
    }
}
