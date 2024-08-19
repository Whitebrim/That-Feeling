using Core.Services.AssetManagement;
using VContainer;

namespace Core.Infrastructure.StateMachine.States
{
    public class MainMenuState : IState
    {
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(){}

        public void Exit()
        {
            AddressablesCache.ReleaseAssets(ReleaseKey.MainMenu);
        }
    }
}