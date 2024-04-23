using System;
using UnityEngine;

namespace UnityBase.Patterns
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : class, new()
    {
        private static readonly Lazy<T> _instance = new (CreateInstance);
        public static T Instance => _instance.Value;

        protected SingletonMonoBehaviour() {}

        private static T CreateInstance()
        {
            GameObject go = new();
            go.name = typeof(T).Name;
            DontDestroyOnLoad(go);

            return go.AddComponent(typeof(T)) as T;
        }
    }
}