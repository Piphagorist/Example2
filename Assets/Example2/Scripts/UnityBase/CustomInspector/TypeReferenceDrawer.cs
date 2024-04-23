using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityBase.CustomInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SG2.UnityBase.CustomInspector
{
    [CustomPropertyDrawer(typeof(TypeReference<>), true)]
    public class TypeReferenceDrawer : PropertyDrawer
    {
        private Type[] _derivedTypes;
        private GUIContent[] _optionLabels;
        private int _selectedIndex;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var storedProperty = property.FindPropertyRelative("qualifiedName");
            string qualifiedName = storedProperty.stringValue;

            if (_optionLabels == null)
            {
                Initialize(property, storedProperty);
            }
            else if (_selectedIndex == _derivedTypes.Length)
            {
                if (qualifiedName != "null") UpdateIndex(storedProperty);
            }
            else
            {
                if (qualifiedName != _derivedTypes[_selectedIndex].AssemblyQualifiedName) UpdateIndex(storedProperty);
            }

            EditorGUI.BeginChangeCheck();

            _selectedIndex = EditorGUI.Popup(position, null, _selectedIndex, _optionLabels);

            if (EditorGUI.EndChangeCheck())
            {
                storedProperty.stringValue = _selectedIndex < _derivedTypes.Length
                    ? _derivedTypes[_selectedIndex].AssemblyQualifiedName
                    : "null";
            }
        }

        private static Type[] FindAllDerivedTypes(Type baseType)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> allTypes = new List<Type>();

            foreach (var assembly in assemblies)
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsInterface || type.IsAbstract)
                        continue;
                    if (type == baseType)
                        continue;
                    if (!baseType.IsAssignableFrom(type))
                        continue;
                    
                    allTypes.Add(type);
                }
            
            return allTypes.ToArray();
        }

        private void Initialize(SerializedProperty property, SerializedProperty stored)
        {
            var baseTypeProperty = property.FindPropertyRelative("baseTypeName");
            var baseType = Type.GetType(baseTypeProperty.stringValue);

            _derivedTypes = FindAllDerivedTypes(baseType);

            if (_derivedTypes.Length == 0)
            {
                _optionLabels = new[] {new GUIContent($"No types derived from {baseType.Name} found.")};
                return;
            }

            _optionLabels = new GUIContent[_derivedTypes.Length + 1];
            for (int i = 0; i < _derivedTypes.Length; i++)
            {
                _optionLabels[i] = new GUIContent(_derivedTypes[i].Name);
            }

            _optionLabels[_derivedTypes.Length] = new GUIContent("null");

            UpdateIndex(stored);
        }

        private void UpdateIndex(SerializedProperty stored)
        {
            string qualifiedName = stored.stringValue;

            for (int i = 0; i < _derivedTypes.Length; i++)
            {
                if (_derivedTypes[i].AssemblyQualifiedName == qualifiedName)
                {
                    _selectedIndex = i;
                    return;
                }
            }

            _selectedIndex = _derivedTypes.Length;
            stored.stringValue = "null";
        }
    }
}