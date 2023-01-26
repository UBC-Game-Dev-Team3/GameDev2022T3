using SymbolBook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol_select_UI : MonoBehaviour
{

    [SerializeField] private Symbol[] known_symbols;
    private Select_slot[] slots;

    private void Start()
    {
        slots = Clue_UI.instance.select_parent.GetComponentsInChildren<Select_slot>();
        fill_symbols();
    }

    public void fill_symbols()
    {
        for(var i = 0; i < slots.Length; i++)
        {
            slots[i].add_symbol(known_symbols[i]);
        }
    }
}
