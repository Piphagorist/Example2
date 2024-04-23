using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityBase.Patterns
{
    [CreateAssetMenu]
    public class SingletonScriptableListContainer : ScriptableObject
    {
        [SerializeField] private List<SingletonScriptableObject> instances;

        private static readonly Lazy<SingletonScriptableListContainer> _instance = new (CreateInstance);
        private static SingletonScriptableListContainer Instance => _instance.Value;

        private SingletonScriptableListContainer() {}

        private static SingletonScriptableListContainer CreateInstance()
        {
            return Resources.LoadAll<SingletonScriptableListContainer>("")[0];
        }

        internal static SingletonScriptableObject GetInstance<T>()
        {
            Type targetType = typeof(T);
            return Instance.instances.Find(e => targetType.IsInstanceOfType(e));
        }
    }
}