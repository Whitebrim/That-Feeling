using Core.Services.SceneLoader;
using VContainer;

namespace Core.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async void Enter(string sceneName) => await SceneLoader.LoadSceneAsync(sceneName, OnLoaded);

        private void OnLoaded()
        {
            //_stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}