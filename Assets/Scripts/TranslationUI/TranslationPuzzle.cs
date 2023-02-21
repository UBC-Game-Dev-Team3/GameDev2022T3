using System;
using SymbolBook;
using UnityEngine;

namespace TranslationUI
{
    /// <summary>
    ///     Represents an item.
    /// </summary>
    [CreateAssetMenu(fileName = "NewTranslationPuzzle", menuName = "Translation/Puzzle")]
    public class TranslationPuzzle : ScriptableObject
    {
        [Tooltip("Object Name")]
        public string objectName = "";
        [Tooltip("Words")]
        public WordPair[] words;

        [Serializable]
        public struct WordPair
        {
            [Tooltip("Word")]
            public Symbol word;
            [Tooltip("Possible options")]
            public string[] options;
            [Tooltip("Hint, empty/null means none")]
            public string hint;
            [Tooltip("Whether this has a fixed hint")]
            public bool startKnown;
        }
    }
}