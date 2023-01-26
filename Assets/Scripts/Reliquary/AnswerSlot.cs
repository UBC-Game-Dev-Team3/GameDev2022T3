using System;
using SymbolBook;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reliquary
{
    public class AnswerSlot : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI description;
        public Action OnOpen;
        public bool pressed = false;

        /// <summary>
        /// Add selected symbol to answer slot
        /// </summary>
        /// <param name="s"></param>
        public void AddSymbol(Symbol s)
        {
            icon.sprite = s.image;
            description.text = s.PlayerSymbolName + " - " + s.PlayerNotes;
        }

        /// <summary>
        /// On answer button click
        /// </summary>
        public void OpenSelectUI()
        {
            if (ClueUI.Instance.symbolselectUI != null)
            {
                ClueUI.Instance.symbolselectUI.SetActive(true);
                pressed = true;
                if(OnOpen!= null)
                {
                    OnOpen();
                }
            }
        }
    }
}
