using System;
using UnityEngine;

namespace UnityBase.CustomInspector
{
    [Serializable]
    public class TypeReference<T> : ISerializationCallbackReceiver {

        [SerializeField] private string qualifiedName;

        public Type Type { get; private set; }

#if UNITY_EDITOR
        // HACK: I wasn't able to find the base type from the SerializedProperty,
        // so I'm smuggling it in via an extra string stored only in-editor.
        [SerializeField] private string baseTypeName;
#endif

        public TypeReference(Type typeToStore) {
            Type = typeToStore;
        }

        public override string ToString() {
            if (Type == null) return string.Empty;
            return Type.Name;
        }

        public void OnBeforeSerialize() {
            qualifiedName = Type?.AssemblyQualifiedName;

#if UNITY_EDITOR
            baseTypeName = typeof(T).AssemblyQualifiedName;
#endif
        }

        public void OnAfterDeserialize() {
            if (string.IsNullOrEmpty(qualifiedName) || qualifiedName == "null") {
                Type = null;
                return;
            }
            Type = Type.GetType(qualifiedName);
        }

        public static implicit operator Type(TypeReference<T> t) => t.Type;

        // TODO: Validate that t is a subtype of T?
        public static implicit operator TypeReference<T>(Type t) => new(t);
    }
}