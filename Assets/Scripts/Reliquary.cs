using SymbolBook;
using UnityEngine;

namespace Util {
    public class Reliquary : Interactable
    {
        [SerializeField] private Puzzle puzzle;

        private Puzzle_slot[] p_slots;
        private Answer_slot[] a_slots;

        private void Start()
        {
            if (Clue_UI.instance != null)
            {
                p_slots = Clue_UI.instance.puzzle_parent.GetComponentsInChildren<Puzzle_slot>();
                a_slots = Clue_UI.instance.answer_parent.GetComponentsInChildren<Answer_slot>();
            }
        }


        public override void Interact(object src, params object[] args)
        {
            base.Interact(src, args);
            open_panel();
        }

        /// <summary>
        /// Fills in puzzle slots in reliquary panel
        /// </summary>
        public void open_panel()
        {
            for (var i = 0; i < p_slots.Length; i++)
            {
                p_slots[i].add_symbol(puzzle.puzzle_symbols[i]);
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Clue_UI.instance.reliquary_ui.SetActive(true);
        }

        /// <summary>
        /// Get symbol selected from select symbol ui
        /// </summary>
        /// <param name="index"></param>
        /// <param name="s"></param>
        public void set_answer_symbol(Symbol s)
        {
            for(var i = 0; i < a_slots.Length; i++)
            {
                if (a_slots[i].pressed)
                {
                    a_slots[i].pressed = false;
                    a_slots[i].add_symbol(s);
                }
            }
        }


        /// <summary>
        /// On activate button press
        /// </summary>
        public void check_answer()
        {
            for(var i = 0; i != puzzle.answer_symbols.Length; i++)
            {
                if (a_slots[i] != puzzle.answer_symbols[i])
                {
                    Debug.Log(i + "th symbol wrong.");
                    return;
                }
            }

            Debug.Log("Complete Level");
        }
    }
}
