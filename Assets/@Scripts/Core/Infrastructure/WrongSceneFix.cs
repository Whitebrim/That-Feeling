using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Infrastructure
{
    public class WrongSceneFix : MonoBehaviour
    {
        private void Awake()
        {
            if (!GameBootstrapper.IsInitialized)
                SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
}
