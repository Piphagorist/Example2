using System;
using System.Collections.Generic;
using SharpBase.DI;
using SharpBase.Tasks;
using UnityBase.CustomInspector;
using UnityBase.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityBase.GameCore
{
    [CreateAssetMenu]
    public class InitOrder : SingletonScriptableObject<InitOrder>
    {
        [SerializeField] private List<TypeReference<IShared>> typesList;
        [SerializeField] private List<Scene> scenesList;
        [SerializeField] private List<TypeReference<IExecutable>> executeList;

        public IReadOnlyList<Scene> ScenesList => scenesList;

        public IEnumerable<Type> GetTypes()
        {
            foreach (var typeReference in typesList)
                yield return typeReference.Type;
        }

        public IEnumerable<Type> GetExecuteTypes()
        {
            foreach (var typeReference in executeList)
                yield return typeReference.Type;
        }
    }
}