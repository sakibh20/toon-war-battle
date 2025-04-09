namespace skb_sec._Project.Scripts.Character
{
    public class CharacterRunningState : CharacterStateBase
    {
        public CharacterRunningState(CharacterStateManager characterStateManager, CharacterStateFactory characterStateFactory) : base(characterStateManager, characterStateFactory)
        {
            IsRootState = true;
            //InitializeSubState();
        }

        public override void EnterState()
        {
            //CustomDebug.Log("EnterState CharacterRunningState");
            CharacterStateManager.cutablesInRangeSo.ClearList();
            CharacterStateManager.characterAnimatorManager.RunAnimation();
            
            InitializeSubState();
        }

        protected override void UpdateState()
        {
            //ustomDebug.Log("CharacterRunningState");
            CheckSwitchStates();
        }

        protected override void ExitState()
        {
            
        }

        public override void CheckSwitchStates()
        {
            if (CharacterStateManager.controllerInputSo.inputInfo.speedModifier == 0)
            {
                SwitchStates(CharacterStateFactory.CharacterIdle());
            }
        }

        public sealed override void InitializeSubState()
        {
            
        }
    }
}
