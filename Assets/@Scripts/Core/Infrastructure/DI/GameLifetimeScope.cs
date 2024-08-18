using Core.Infrastructure.States;
using Core.Services;
using Core.Services.AssetManagement;
using Core.Services.Audio;
using Core.Signals;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Infrastructure.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private AudioSystem audioSystem;
        [SerializeField] private MainThreadDispatcher mainThreadDispatcher;
        [SerializeField] private ConditionalAssetManager conditionalAssetManager;

        protected override void Configure(IContainerBuilder builder)
        {
            if (GameBootstrapper.IsInitialized) return;
            builder.RegisterEntryPoint<GameBootstrapper>(); // <-- Application entry point

            builder.Register<IObjectResolver, Container>(Lifetime.Scoped);
            
            builder.RegisterComponent(audioSystem);
            builder.RegisterComponent(mainThreadDispatcher);
            
            builder.RegisterInstance(conditionalAssetManager);

            builder.Register<GameStateMachine>(Lifetime.Singleton);
            builder.Register<BootstrapState>(Lifetime.Singleton);
            builder.Register<LoadLevelState>(Lifetime.Singleton);
            builder.Register<GameLoopState>(Lifetime.Singleton);
            
            RegisterMessagePipe(builder);
        }

        private void RegisterMessagePipe(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();
            
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));

            builder.RegisterMessageBroker<OnApplicationFocusSignal>(options);
        }
    }
}
