using System;
using Core.Infrastructure.StateMachine;
using UnityEngine;
using VContainer;

namespace UI.Mediator
{
    public abstract class Mediator : MonoBehaviour
    {
        [Inject] protected readonly GameStateMachine StateMachine;

        protected IDisposable Disposable;

        public void OnDestroy()
        {
            Disposable?.Dispose();
        }
    }
}
