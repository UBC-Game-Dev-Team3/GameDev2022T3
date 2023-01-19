using System;
using UnityEngine;

namespace SymbolBook
{
    /// <summary>
    ///     Represents a symbol for the symbol book
    /// </summary>
    [CreateAssetMenu(fileName = "New Word", menuName = "SymbolBook/Word")]
    public class Word : ScriptableObject
    {
        [Tooltip("Name of Word (hidden to user)")]
        public string wordName = "New Word";
        /// <summary>
        /// Word name given by the user. Is likely incorrect.
        /// </summary>
        [NonSerialized]
        public string PlayerWordName = "";
        /// <summary>
        /// Player notes written by the user. Is also likely incorrect.
        /// </summary>
        [NonSerialized]
        public string PlayerNotes = "";
        [Tooltip("Whether this starts seen.")]
        public bool startSeen; 
        /// <summary>
        /// Whether the player saw this.
        /// </summary>
        [NonSerialized] public bool SeenByPlayer;
        [Tooltip("List of Symbols this Makes Up")]
        public Symbol[] contents;
    }
}