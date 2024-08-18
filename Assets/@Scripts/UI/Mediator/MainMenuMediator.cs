using Core.Infrastructure.States;
using Core.Services.SceneLoader;
using Sirenix.OdinInspector;
using Utils;

namespace UI.Mediator
{
    public class MainMenuMediator : Mediator
    {
        [Button("Play")]
        public void Play()
        {
            StateMachine.Enter<LoadLevelState, string>(SceneNameConstants.Game);
        }
    }
}