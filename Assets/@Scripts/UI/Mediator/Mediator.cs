using Core.Infrastructure.StateMachine;
using UnityEngine;
using VContainer;

namespace UI.Mediator
{
    public abstract class Mediator : MonoBehaviour
    {
        protected GameStateMachine StateMachine;

        [Inject]
        protected void Construct(GameStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}
