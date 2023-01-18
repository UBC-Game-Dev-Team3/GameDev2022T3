using SymbolBook;
using UnityEditor;
using UnityEngine;

namespace CEditor
{
    /// <summary>
    /// Custom inspector for SymbolBookUI
    /// </summary>
    [CustomEditor(typeof(SymbolBookUI))] 
    public class SymbolBookUIEditor : Editor
    {
        // ReSharper disable once UnusedMember.Local
        private static readonly string[] DontInclude = new string[] {"m_Script"};

        /// <summary>
        /// Runs upon it being shown in editor
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            SymbolBookUI script = (SymbolBookUI) target;
            DrawPropertiesExcluding(serializedObject, DontInclude);
            if (GUILayout.Button("Random"))
            {
                var symbols = SymbolManager.Instance.symbols;
                script.OnButtonClick(symbols[Random.Range(0,symbols.Length)].symbolName);
            }
            if (EditorGUI.EndChangeCheck()) serializedObject.ApplyModifiedProperties();
        }
    }
}