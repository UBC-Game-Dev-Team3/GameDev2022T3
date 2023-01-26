using SymbolBook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class Clue_object : Interactable
    {
        public Clue clue;
        public Symbol_slot[] slots;
        public bool selected;
        private void Start()
        {
            if (Clue_UI.instance != null)
            {
                slots = Clue_UI.instance.symbol_parent.GetComponentsInChildren<Symbol_slot>();
                Debug.Log("Test");
            }

            selected= false;
        }

        /// <summary>
        /// Fills in slots in clue panel
        /// </summary>
        public void open_panel()
        {
            for(var i = 0; i < slots.Length; i++)
            {
                slots[i].add_symbol(clue.symbols[i]);
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Clue_UI.instance.clue_ui.SetActive(true);
            selected = true;
        }

        public override void Interact(object src, params object[] args)
        {
            base.Interact(src, args);
            open_panel();
        }
  
    }
}
