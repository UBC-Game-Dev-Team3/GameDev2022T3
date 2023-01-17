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
        [Tooltip("Symbol Name from user")]
        public string PlayerSymbolName = "";
        [Tooltip("Take a wild guess")]
        public Sprite image;
        [TextArea, Tooltip("Player's notes")]
        public string PlayerNotes = "";

        [Tooltip("Check if part of given symbol used to solve unknown ones")]
        public bool given;
    }
}