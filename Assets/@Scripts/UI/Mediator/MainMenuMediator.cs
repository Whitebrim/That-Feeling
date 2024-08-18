using Core.Infrastructure.StateMachine.States;
using Core.Services.SceneLoader;
using Sirenix.OdinInspector;

namespace UI.Mediator
{
    public class MainMenuMediator : Mediator
    {
        [Button("Play")]
        public async void Play()
        {
            await SceneLoader.LoadSceneAsync(SceneNameConstants.Game);
            StateMachine.Enter<GameLoopState>();
        }
    }
}