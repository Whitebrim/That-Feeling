using Core.Services.AssetManagement;
using VContainer;

namespace Core.Infrastructure.StateMachine.States
{
    public class Level1State : IState
    {
        [Inject] private readonly GameStateMachine _stateMachine;

        public void Enter(){}

        public void Exit()
        {
            AddressablesCache.ReleaseAssets(ReleaseKey.Level1);
        }
    }
}