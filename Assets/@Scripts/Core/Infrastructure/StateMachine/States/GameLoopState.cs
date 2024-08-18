using VContainer;

namespace Core.Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(){}

        public void Exit(){}
    }
}