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
                _symbol.Render(sprite.transform.gameObject, 100);
                if (text == null) return;
                text.text = string.IsNullOrWhiteSpace(_symbol.PlayerSymbolName) ? "???" : _symbol.PlayerSymbolName;
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
        
        public void OnButtonClick()
        {
            if (ui != null && DisplayedSymbol != null)
            {
                ui.OnButtonClick(DisplayedSymbol.symbolName);
            }
        }
    }
}