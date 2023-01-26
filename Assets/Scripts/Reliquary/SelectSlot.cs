using SymbolBook;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Reliquary
{
    public class SelectSlot : MonoBehaviour
    {
        [SerializeField] private Button iconButton;
        [SerializeField] private TextMeshProUGUI nameText;
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

            nameText.text = s.symbolName;
        }

        /// <summary>
        /// On symbol press
        /// </summary>
        public void select_symbol()
        {
            selected = true;
            iconButton.GetComponent<Image>().color = Color.green;
        }

        /// <summary>
        /// Sets symbol selected
        /// </summary>
        public void pass_symbol()
        {
            FindObjectOfType<Reliquary>().set_answer_symbol(_symbol);
            ClueUI.Instance.symbolselectUI.SetActive(false);
        }

    }
}
