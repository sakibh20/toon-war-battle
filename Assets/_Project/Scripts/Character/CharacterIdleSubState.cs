namespace skb_sec._Project.Scripts.Character
{
    public class CharacterIdleSubState : CharacterStateBase
    {
        public CharacterIdleSubState(CharacterStateManager characterStateManager, CharacterStateFactory characterStateFactory) : base(characterStateManager, characterStateFactory)
        {
        }

        public override void EnterState()
        {
            //CustomDebug.Log("EnterState CharacterIdleSubState");
            CharacterStateManager.characterAnimatorManager.IdleAnimation();
        }

        protected override void UpdateState()
        {
            //CustomDebug.Log("CharacterIdleSubState");
            CheckSwitchStates();
        }

        protected override void ExitState()
        {
            //CustomDebug.Log("ExitState CharacterIdleSubState");
            // RemoveSuperState();
        }

        public override void CheckSwitchStates()
        {
            //CustomDebug.Log(CharacterStateManager.cutablesInRangeSo.AllCutablesCountInRange.ToString());
            if (CharacterStateManager.cutablesInRangeSo.AllInteractableCountInRange > 0)
            {
                SwitchStates(CharacterStateFactory.CharacterCuttingSubState());
            }
        }

        public override void InitializeSubState()
        {
        }
    }
}
