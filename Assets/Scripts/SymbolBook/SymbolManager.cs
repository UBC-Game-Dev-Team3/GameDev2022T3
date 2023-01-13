using System;
using UnityEngine;

namespace SymbolBook
{
    [DisallowMultipleComponent]
    public class SymbolManager : MonoBehaviour
    {
        #region Singleton Design Pattern
        /// <summary>
        ///     Singleton instance.
        /// </summary>
        private static SymbolManager _privateStaticInstance;

        /// <summary>
        ///     Getter for the singleton.
        /// </summary>
        public static SymbolManager Instance
        {
            get
            {
                if (_privateStaticInstance == null) _privateStaticInstance = FindObjectOfType<SymbolManager>();
                if (_privateStaticInstance != null) return _privateStaticInstance;
                GameObject go = new GameObject("Symbol Manager");
                _privateStaticInstance = go.AddComponent<SymbolManager>();
                return _privateStaticInstance;
            }
        }
        #endregion

        public string allSymbolsPath = "Symbols";
        
        [Tooltip("List of symbols.")]
        public Symbol[] symbols = Array.Empty<Symbol>();

        /// <summary>
        ///     On awake, set this to private static instance and load the symbols.
        /// </summary>
        private void Awake()
        {
            if (_privateStaticInstance != null && _privateStaticInstance != this)
                Debug.LogWarning("More than one instance of Inventory found!");
            else _privateStaticInstance = this;
            
            symbols = Resources.LoadAll<Symbol>(allSymbolsPath);
        }
    }
}