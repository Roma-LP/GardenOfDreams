using UnityEngine;

namespace _GardenOfDreams.Scripts.Utilities
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool _isShuttingDown = false;

        public static T Instance
        {
            get
            {
                if (_isShuttingDown)
                {
                    Debug.LogWarning($"[Singleton] Instance of {typeof(T)} was requested while shutting down.");
                    return null;
                }

                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        Debug.LogError($"[Singleton] No instance of {typeof(T)} found in the scene. Please add one manually.");
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != this)
            {
                Debug.LogError($"[Singleton] Duplicate instance of {typeof(T)} detected. Destroying duplicate.");
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _isShuttingDown = true;
                _instance = null;
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _isShuttingDown = true;
        }
    }
}