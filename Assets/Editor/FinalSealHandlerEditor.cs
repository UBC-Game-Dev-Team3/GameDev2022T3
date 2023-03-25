using Reliquary;
using UnityEditor;
using UnityEngine;

namespace CEditor
{
    [CustomEditor(typeof(FinalSealHandler))]
    public class FinalSealHandlerEditor : Editor
    {
        private static readonly string[] DontInclude = new string[] {"m_Script"};
        /// <summary>
        /// Runs upon it being shown in editor
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawPropertiesExcluding(serializedObject, DontInclude);
            FinalSealHandler handler = (FinalSealHandler) target;
            if (GUILayout.Button("Trigger Full Success"))
            {
                handler.TriggerPuzzleSuccess();
            }
            if (GUILayout.Button("Trigger Success"))
            {
                handler.OnSuccess();
            }
            if (GUILayout.Button("Trigger Failure"))
            {
                handler.OnFailure();
            }
            if (EditorGUI.EndChangeCheck()) serializedObject.ApplyModifiedProperties();
        }
    }
}