using System;
using UnityEngine;

namespace SymbolBook
{
    /// <summary>
    ///     Represents a symbol for the symbol book
    /// </summary>
    [CreateAssetMenu(fileName = "New Symbol", menuName = "SymbolBook/Symbol")]
    public class Symbol : ScriptableObject
    {
        [Tooltip("Name of Symbol (hidden to user)")]
        public string symbolName = "New Symbol";
        /// <summary>
        /// Symbol name given by the user. Is likely incorrect.
        /// </summary>
        [NonSerialized]
        public string PlayerSymbolName = "";
        [Tooltip("Take a wild guess")]
        public Sprite image;
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