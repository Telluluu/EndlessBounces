using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool ApplicationIsQuitting;
        private static readonly object _locker = new();

        private void OnApplicationQuit()
        {
            ApplicationIsQuitting = true;
        }

        public static T Instance
        {
            get
            {
                if (ApplicationIsQuitting)
                {
                    throw new System.Exception("Application is quiting");
                }

                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = FindObjectOfType<T>();
                            if (_instance == null)
                            {
                                GameObject go = new GameObject("Singleton " + typeof(T));
                                _instance = go.AddComponent<T>();
                            }
                        }
                    }
                }
                return _instance;
            }
        }
    }
}