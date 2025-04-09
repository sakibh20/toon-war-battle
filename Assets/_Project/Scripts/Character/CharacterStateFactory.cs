namespace skb_sec._Project.Scripts.Character
{
    public class CharacterStateFactory
    {
        private CharacterStateManager _context;
        
        public CharacterStateFactory(CharacterStateManager context)
        {
            _context = context;
        }
        
        public CharacterStateBase CharacterIdle()
        {
            return new CharacterIdleState(_context, this);
        }
        
        public CharacterStateBase CharacterLay()
        {
            return new CharacterLayState(_context, this);
        }
        
        public CharacterStateBase CharacterRun()
        {
            return new CharacterRunningState(_context, this);
        }
        
        public CharacterStateBase CharacterCuttingSubState()
        {
            return new CharacterCuttingSubState(_context, this);
        } 
        
        public CharacterStateBase CharacterIdleSubState()
        {
            return new CharacterIdleSubState(_context, this);
        }
    }
}
