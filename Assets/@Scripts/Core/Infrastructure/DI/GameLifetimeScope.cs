using Core.Infrastructure.States;
using Core.Services;
using Core.Services.AssetManagement;
using Core.Services.Audio;
using Core.Services.SceneLoader;
using Core.Signals;
using MessagePipe;
using UnityEngine;
using Utils;
using VContainer;
using VContainer.Unity;


namespace Core.Infrastructure.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameBootstrapper gameBootstrapper;
        [SerializeField] private AudioSystem audioSystem;
        [SerializeField] private MainThreadDispatcher mainThreadDispatcher;
        [SerializeField] private ApplicationFocus applicationFocus;
        [SerializeField] private ConditionalAssetManager conditionalAssetManager;

        protected override void Configure(IContainerBuilder builder)
        {
            if (GameBootstrapper.IsInitialized) return;

            DontDestroyOnLoad(gameObject);

            builder.Register<IObjectResolver, Container>(Lifetime.Scoped);

            builder.RegisterComponent(gameBootstrapper).AsImplementedInterfaces();
            builder.RegisterComponent(audioSystem);
            builder.RegisterComponent(mainThreadDispatcher);
            builder.RegisterComponent(applicationFocus);

            builder.Register<BootstrapState>(Lifetime.Singleton);
            builder.Register<LoadLevelState>(Lifetime.Singleton);
            builder.Register<GameLoopState>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);

            builder.Register<SceneLoader>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterInstance(conditionalAssetManager);

            RegisterMessagePipe(builder);
        }

        private void RegisterMessagePipe(IContainerBuilder builder)
        {
            // RegisterMessagePipe returns options.
            var options = builder.RegisterMessagePipe(/* configure option */);
        
            // Setup GlobalMessagePipe to enable diagnostics window and global function
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));

            builder.RegisterMessageBroker<OnApplicationFocusSignal>(options);
        }
    }
}
