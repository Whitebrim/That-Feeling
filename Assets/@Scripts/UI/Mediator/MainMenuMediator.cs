using Core.Infrastructure.States;
using Sirenix.OdinInspector;
using Utils;

namespace UI.Mediator
{
    class MainMenuMediator : Mediator
    {
        [Button("Play")]
        public void Play()
        {
            StateMachine.Enter<LoadLevelState, string>(SceneNameConstants.Game);
        }
    }
}