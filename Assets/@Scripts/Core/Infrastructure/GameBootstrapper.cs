using System;
using Core.Infrastructure.States;
using Core.Services;
using Core.Services.AssetManagement;
using Core.Services.Audio;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Core.Infrastructure
{
    [RequireComponent(typeof(MainThreadDispatcher), typeof(AudioSystem))]
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner, IStartable
    {
        public static bool IsInitialized;

        [SerializeField] private ConditionalAssetManager assetManager;
        
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        void IStartable.Start()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                if (!IsInitialized) SceneManager.LoadScene(0);
                Destroy(gameObject);
                return;
            }
            ApplicationInit();

            DontDestroyOnLoad(this);

            IsInitialized = true;

            _stateMachine.Enter<BootstrapState>();
        }

        private void ApplicationInit()
        {
            Application.targetFrameRate = (int)Math.Ceiling(Screen.currentResolution.refreshRateRatio.value);
            Addressables.InitializeAsync();
        }
    }
}