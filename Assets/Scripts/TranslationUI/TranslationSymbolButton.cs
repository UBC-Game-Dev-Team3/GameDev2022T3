using System;
using SymbolBook;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TranslationUI
{
    public class TranslationSymbolButton : MonoBehaviour
    {
        [NonSerialized]
        public TranslationScreen ui;

        public Symbol DisplayedSymbol
        {
            get => _symbol;
            set
            {
                _symbol = value;
                _symbol.Render(sprite.transform.gameObject, 100);
                if (text == null) return;
                text.text = string.IsNullOrWhiteSpace(_symbol.PlayerSymbolName) ? Symbol.SymbolDefaultName : _symbol.PlayerSymbolName;
            }
        }

        private Symbol _symbol;
        public Image sprite;
        public TMP_Text text;

        private void Awake()
        {
            if (_symbol == null) return;
            sprite.sprite = _symbol.image;
            
            if (text != null)
                text.text = _symbol.PlayerSymbolName;
        }

        public void UpdateFromHint(TranslationPuzzle.WordPair word)
        {
            if (string.IsNullOrWhiteSpace(word.hint)) return;
            if (!string.IsNullOrWhiteSpace(_symbol.PlayerSymbolName) &&
                _symbol.PlayerSymbolName != Symbol.SymbolDefaultName) return;
            if (word.startKnown) text.text = word.hint;
            else text.text = word.hint + '?';
        }
        
        public void OnButtonClick()
        {
            if (ui != null && DisplayedSymbol != null)
            {
                ui.OnSymbolClick(DisplayedSymbol.symbolName);
            }
        }
    }
}