namespace StateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update(float deltaTime);
        
        public string ToString();
    }
}
