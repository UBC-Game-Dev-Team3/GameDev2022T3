using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SymbolBook
{
    public class SymbolBookButton : MonoBehaviour
    {
        public SymbolBookUI ui;

        public Symbol DisplayedSymbol
        {
            get => _symbol;
            set
            {
                _symbol = value;
                sprite.sprite = _symbol.image;
                text.text = _symbol.PlayerSymbolName;
                Debug.Log("HEHE: SETTING TEXT TO" + text.text);
            }
        }

        private Symbol _symbol;
        public Image sprite;
        public TMP_Text text;

        private void Awake()
        {
            if (_symbol == null) return;
            sprite.sprite = _symbol.image;
            text.text = _symbol.PlayerSymbolName;
            Debug.Log("HEHE: SETTING TEXT TO" + text.text);
        }
        
        public void OnButtonClick()
        {
            if (ui != null && DisplayedSymbol != null)
            {
                ui.OnButtonClick(DisplayedSymbol.symbolName);
            }
        }
    }
}