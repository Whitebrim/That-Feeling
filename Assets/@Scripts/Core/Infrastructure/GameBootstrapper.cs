using System;
using Core.Infrastructure.States;
using Core.Services;
using Core.Services.AssetManagement;
using Core.Services.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Core.Infrastructure
{
    [RequireComponent(typeof(MainThreadDispatcher), typeof(AudioSystem))]
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public static bool IsInitialized;

        [SerializeField] private ConditionalAssetManager assetManager;
        
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        // ReSharper disable once Unity.IncorrectMethodSignature
        private async UniTaskVoid Start()
        {
            IsInitialized = true;
        
            await ApplicationInit();

            DontDestroyOnLoad(this);

            _stateMachine.Enter<BootstrapState>();
        }
        
        private async UniTask ApplicationInit()
        {
            Application.targetFrameRate = (int)Math.Ceiling(Screen.currentResolution.refreshRateRatio.value);
            await Addressables.InitializeAsync();
        }
    }
}