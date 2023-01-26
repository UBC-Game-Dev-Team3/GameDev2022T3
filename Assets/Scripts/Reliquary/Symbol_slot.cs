using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SymbolBook;
using Util;

public class Symbol_slot : MonoBehaviour
{
    [SerializeField] private Button icon_button;
    [SerializeField] private TMP_InputField input_field;
    [SerializeField] private TextMeshProUGUI name_text;
    [SerializeField] private TMP_InputField input_notes;
    private Symbol symbol;
    public bool selected = false;

    /// <summary>
    /// Fill in slot with symbol and description if given
    /// </summary>
    /// <param name="s"></param>
    public void add_symbol(Symbol s)
    {
        symbol = s;
        icon_button.GetComponent<Image>().sprite = s.image;
        if (s.given)
        {
            input_field.enabled= false;
            name_text.text = s.symbolName;
            icon_button.interactable = false;
        } else
        {
            name_text.enabled = false;
        }
    }

    /// <summary>
    /// On symbol press
    /// </summary>
    public void select_symbol()
    {
        selected = true;
        icon_button.GetComponent<Image>().color = Color.green;
    }

    public string get_name()
    {
        return input_field.text;
    }


    public string get_notes()
    {
        return input_notes.text;
    }

    /// <summary>
    /// Show warning if no symbol selected
    /// </summary>
    public void show_warning()
    {
        input_notes.text += "Please select a symbol first before recording.";
    }


    /// <summary>
    /// Show added message
    /// </summary>
    public void show_added()
    {
        input_notes.text = "Name and notes added to symbol.";
    }
}
