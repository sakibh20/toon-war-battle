namespace skb_sec._Project.Scripts.Character
{
    public abstract class CharacterStateBase
    {
        private bool _isRootState;

        protected bool IsRootState
        {
            set => _isRootState = value;
        }
        protected CharacterStateManager CharacterStateManager { get; }

        protected CharacterStateFactory CharacterStateFactory { get; }

        private CharacterStateBase _currentSubState;
        private CharacterStateBase _currentSuperState;

        protected CharacterStateBase(CharacterStateManager characterStateManager, CharacterStateFactory characterStateFactory)
        {
            CharacterStateManager = characterStateManager;
            CharacterStateFactory = characterStateFactory;
        }
        public abstract void EnterState();
        protected abstract void UpdateState();

        protected abstract void ExitState();

        public abstract void CheckSwitchStates();
        public abstract void InitializeSubState();

        public void UpdateStates()
        {
            UpdateState();
            if (_currentSubState != null)
            {
                _currentSubState.UpdateStates();
            }
        }

        private void ExitStates()
        {
            ExitState();
            if (_currentSubState != null)
            {
                _currentSubState.ExitStates();
            }
        }

        protected void SwitchStates(CharacterStateBase newState)
        {
            //CustomDebug.Log("newState: " + newState);

            ExitStates();
            
            // newState.EnterState();

            if (_isRootState)
            {
                newState.EnterState();
                RemoveSubState();
                CharacterStateManager.CharacterCurrentState = newState;
                //CustomDebug.Log("CharacterStateManager.CharacterCurrentState = newState");
            }
            else if (_currentSuperState != null)
            {
                _currentSuperState.SetSubState(newState);
                //CustomDebug.Log("_currentSuperState.SetSubState(newState);");
            }
        }

        private void SetSuperState(CharacterStateBase newSuperState)
        {
            _currentSuperState = newSuperState;
        }

        protected void SetSubState(CharacterStateBase newSubState)
        {
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
            newSubState.EnterState();
        }

        private void RemoveSubState()
        {
            if (_currentSubState != null)
            {
                _currentSubState._currentSubState = null;
                _currentSubState = null;
            }
        }
    }
}
