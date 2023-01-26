using SymbolBook;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Reliquary
{
    public class SymbolSlot : MonoBehaviour
    {
        [FormerlySerializedAs("icon_button")] [SerializeField] private Button iconButton;
        [FormerlySerializedAs("input_field")] [SerializeField] private TMP_InputField inputField;
        [FormerlySerializedAs("name_text")] [SerializeField] private TextMeshProUGUI nameText;
        [FormerlySerializedAs("input_notes")] [SerializeField] private TMP_InputField inputNotes;
        private Symbol _symbol;
        public bool selected = false;

        /// <summary>
        /// Fill in slot with symbol and description if given
        /// </summary>
        /// <param name="s"></param>
        public void add_symbol(Symbol s)
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

        public string get_name()
        {
            return inputField.text;
        }


        public string get_notes()
        {
            return inputNotes.text;
        }

        /// <summary>
        /// Show warning if no symbol selected
        /// </summary>
        public void show_warning()
        {
            inputNotes.text += "Please select a symbol first before recording.";
        }


        /// <summary>
        /// Show added message
        /// </summary>
        public void show_added()
        {
            inputNotes.text = "Name and notes added to symbol.";
        }
    }
}
