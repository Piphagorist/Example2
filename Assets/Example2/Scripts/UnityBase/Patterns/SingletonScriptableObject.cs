using System;
using UnityEngine;

namespace UnityBase.Patterns
{
    public class SingletonScriptableObject : ScriptableObject { }
    
    public class SingletonScriptableObject<T> : SingletonScriptableObject where T : class
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(FindInstance);
        public static T Instance => _instance.Value;

        protected SingletonScriptableObject() {}

        private static T FindInstance()
        {
            return SingletonScriptableListContainer.GetInstance<T>() as T;
        }
    }
}