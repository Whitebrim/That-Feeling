using UnityEngine;

namespace Utils
{
    public class LifeTime : MonoBehaviour
    {
        public float Life = 0.1f;

        void Start()
        {
            Destroy(gameObject, Life);
        }

    }
}