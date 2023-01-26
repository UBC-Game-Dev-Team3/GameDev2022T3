using SymbolBook;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class Select_slot : MonoBehaviour
{
    [SerializeField] private Button icon_button;
    [SerializeField] private TextMeshProUGUI name_text;
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

        name_text.text = s.symbolName;
    }

    /// <summary>
    /// On symbol press
    /// </summary>
    public void select_symbol()
    {
        selected = true;
        icon_button.GetComponent<Image>().color = Color.green;
    }

    /// <summary>
    /// Sets symbol selected
    /// </summary>
    public void pass_symbol()
    {
        FindObjectOfType<Reliquary>().set_answer_symbol(symbol);
        Clue_UI.instance.symbolselect_ui.SetActive(false);
    }

}
