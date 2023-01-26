using SymbolBook;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reliquary
{
    public class SymbolSlot : MonoBehaviour
    {
        [SerializeField] private Button iconButton;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TMP_InputField inputNotes;
        private Symbol _symbol;
        public bool selected = false;

        /// <summary>
        /// Fill in slot with symbol and description if given
        /// </summary>
        /// <param name="s"></param>
        public void AddSymbol(Symbol s)
        {
            _symbol = s;
            iconButton.GetComponent<Image>().sprite = s.image;
            if (s.given)
            {
                inputField.enabled= false;
                nameText.text = s.symbolName;
                iconButton.interactable = false;
            } else
            {
                nameText.enabled = false;
            }
        }

        /// <summary>
        /// On symbol press
        /// </summary>
        public void select_symbol()
        {
            selected = true;
            iconButton.GetComponent<Image>().color = Color.green;
        }

        public string GetName()
        {
            return inputField.text;
        }


        public string GetNotes()
        {
            return inputNotes.text;
        }

        /// <summary>
        /// Show warning if no symbol selected
        /// </summary>
        public void ShowWarning()
        {
            inputNotes.text += "Please select a symbol first before recording.";
        }


        /// <summary>
        /// Show added message
        /// </summary>
        public void ShowAdded()
        {
            inputNotes.text = "Name and notes added to symbol.";
        }
    }
}
