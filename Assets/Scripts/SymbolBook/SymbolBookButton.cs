using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SymbolBook
{
    public class SymbolBookButton : MonoBehaviour
    {
        [NonSerialized]
        public SymbolBookUI ui;

        public bool DisplayAsBlack { get; set; } = false;

        public Symbol DisplayedSymbol
        {
            get => _symbol;
            set
            {
                _symbol = value;
                _symbol.Render(sprite.transform.gameObject, shouldBeBlack: DisplayAsBlack);
                if (text == null) return;
                text.text = string.IsNullOrWhiteSpace(_symbol.PlayerSymbolName) ? "???" : _symbol.PlayerSymbolName;
            }
        }

        public bool Highlighted
        {
            set
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject.name != "Highlight") continue;
                    if (!value) Destroy(transform.GetChild(i).gameObject);
                    return;
                }

                if (value)
                {
                    Instantiate(highlight, transform);
                }
            }
        }

        private Symbol _symbol;
        public Image sprite;
        public GameObject highlight;
        public TMP_Text text;

        private void Awake()
        {
            if (_symbol == null) return;
            sprite.sprite = _symbol.image;
            
            if (text != null)
                text.text = _symbol.PlayerSymbolName;
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