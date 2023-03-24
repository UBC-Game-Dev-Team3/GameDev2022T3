using System;
using System.Collections.Generic;
using System.Linq;
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

        [NonSerialized]
        public Dictionary<string, Symbol> SymbolLookup = new();

        [NonSerialized]
        public Dictionary<string, List<Symbol>> AppearsInLookup = new();

        /// <summary>
        ///     On awake, set this to private static instance and load the symbols.
        /// </summary>
        private void Awake()
        {
            if (_privateStaticInstance != null && _privateStaticInstance != this)
                Debug.LogWarning("More than one instance of Inventory found!");
            else _privateStaticInstance = this;
            
            symbols = Resources.LoadAll<Symbol>(allSymbolsPath);
            foreach (Symbol s in symbols)
            {
                s.SeenByPlayer = s.startSeen;
            }
            SymbolLookup = symbols.ToDictionary(symbol => symbol.symbolName, symbol => symbol);
            ComputeAppearsIn();
        }

        private void ComputeAppearsIn()
        {
            AppearsInLookup.Clear();
            foreach (Symbol s in symbols)
            {
                if (!AppearsInLookup.ContainsKey(s.symbolName))
                {
                    AppearsInLookup[s.symbolName] = new List<Symbol> {s};
                }

                if (s.contents == null || s.contents.Length == 0) continue;

                foreach (Symbol child in s.contents)
                {
                    if (child == null)
                    {
                        Debug.LogWarning(s.symbolName + " has null child. Skipping.");
                        continue;
                    }
                    if (!AppearsInLookup.ContainsKey(child.symbolName))
                    {
                        AppearsInLookup[child.symbolName] = new List<Symbol> {child};
                    }
                    AppearsInLookup[child.symbolName].Add(s);
                }
            }
        }

        public int SymbolIndex(string s)
        {
            for (int i = 0; i < symbols.Length; i++)
            {
                if (symbols[i] != null && symbols[i].symbolName == s)
                {
                    return i;
                }
            }

            return -1;
        }

        public Symbol[] SeenSymbols()
        {
            return symbols.Where(s => s != null && s.SeenByPlayer).ToArray();
        }
        
        public Symbol[] SeenParents(Symbol symbol)
        {
            return AppearsInLookup[symbol.symbolName].Where(s => s != null && s.SeenByPlayer).ToArray();
        }
    }
}