using SymbolBook;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Answer_slot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI description;
    public Action onOpen;
    public bool pressed = false;

    /// <summary>
    /// Add selected symbol to answer slot
    /// </summary>
    /// <param name="s"></param>
    public void add_symbol(Symbol s)
    {
        icon.sprite = s.image;
        description.text = s.PlayerSymbolName + " - " + s.PlayerNotes;
    }

    /// <summary>
    /// On answer button click
    /// </summary>
    public void open_select_ui()
    {
        if (Clue_UI.instance.symbolselect_ui != null)
        {
            Clue_UI.instance.symbolselect_ui.SetActive(true);
            pressed = true;
            if(onOpen!= null)
            {
                onOpen();
            }
        }
    }
}
