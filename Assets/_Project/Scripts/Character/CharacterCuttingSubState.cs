namespace skb_sec._Project.Scripts.Character
{
    public class CharacterCuttingSubState : CharacterStateBase
    {
        public CharacterCuttingSubState(CharacterStateManager characterStateManager, CharacterStateFactory characterStateFactory) 
            : base(characterStateManager, characterStateFactory) {}

        public override void EnterState()
        {
            //CustomDebug.Log("EnterState CharacterCuttingSubState");
            CharacterStateManager.toolSo.FireEquipCuttingTool();
            CharacterStateManager.characterAnimatorManager.CuttingAnimation();
        }

        protected override void UpdateState()
        {
            //CustomDebug.Log("CharacterCuttingSubState");
            CheckSwitchStates();
        }

        protected override void ExitState()
        {
            //CustomDebug.Log("ExitState CharacterCuttingSubState");
            // RemoveSuperState();
            CharacterStateManager.toolSo.FireUnEquipCuttingTool();
        }

        public override void CheckSwitchStates()
        {
            if (CharacterStateManager.cutablesInRangeSo.AllInteractableCountInRange <= 0)
            {
                SwitchStates(CharacterStateFactory.CharacterIdleSubState());
            }
        }

        public override void InitializeSubState()
        {
        }
    }
}
