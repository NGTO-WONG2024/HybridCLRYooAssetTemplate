using UnityEngine;

namespace Script.Scripts_HotUpdate
{
    /// <summary>
    /// 泛型 Singleton MonoBehaviour 基类。
    /// </summary>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object LockObject = new object();
        private static bool _applicationIsQuitting = false;

        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed on application quit. Won't create again - returning null.");
                    return null;
                }

                lock (LockObject)
                {
                    if (_instance != null) return _instance;
                    _instance = FindFirstObjectByType<T>();

                    if (FindObjectsByType<T>(FindObjectsSortMode.InstanceID).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went very wrong " +
                                       " - there should never be more than 1 singleton! Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                                  " is needed in the scene, so '" + singleton +
                                  "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " +
                                  _instance.gameObject.name);
                    }

                    return _instance;
                }
            }
        }

        protected virtual void OnDestroy()
        {
            _applicationIsQuitting = true;
        }
    }
}