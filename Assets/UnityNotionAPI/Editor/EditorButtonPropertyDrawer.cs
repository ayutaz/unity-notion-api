using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace BennyKok.NotionAPI.Editor
{ 
    [CustomPropertyDrawer(typeof(EditorButtonAttribute))]
    public class EditorButtonPropertyDrawer : PropertyDrawer
    {
        private MethodInfo _eventMethodInfo = null;

        public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
        {
            var button = (EditorButtonAttribute)attribute;

            if (GUI.Button(rect, button.label))
            {
                var type = prop.serializedObject.targetObject.GetType();

                if (_eventMethodInfo == null)
                    _eventMethodInfo = type.GetMethod(button.methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                if (_eventMethodInfo != null)
                    _eventMethodInfo.Invoke(prop.serializedObject.targetObject, null);
                else
                    Debug.LogWarning(string.Format("Unable to find method {0} in {1}", button.methodName, type));
            }
        }
    }
}
