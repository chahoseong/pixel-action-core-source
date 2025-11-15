using StateMachine;

namespace Character.Player.States
{
    public abstract class PlayerState : IState
    {
        protected PlayerCharacter Context { get; }

        protected PlayerState(PlayerCharacter context)
        {
            Context = context;
        }

        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
        }

        public virtual void Update(float deltaTime)
        {
        }
        
        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
