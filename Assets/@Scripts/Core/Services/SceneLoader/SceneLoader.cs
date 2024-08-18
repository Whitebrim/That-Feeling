using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Core.Services.SceneLoader
{
    public static class SceneLoader
    {
        public static async UniTask LoadSceneAsync(string sceneName, Action onLoad)
        {
            if (SceneManager.GetActiveScene().name != sceneName)
            {
                await SceneManager.LoadSceneAsync(sceneName);
            }

            onLoad?.Invoke();
        }
    }
}