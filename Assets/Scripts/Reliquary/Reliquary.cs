using SymbolBook;
using UnityEngine;
using Util;

namespace Reliquary {
    public class Reliquary : Interactable
    {
        [SerializeField] private Puzzle puzzle;

        private PuzzleSlot[] _pSlots;
        private AnswerSlot[] _aSlots;

        private void Start()
        {
            if (ClueUI.Instance != null)
            {
                _pSlots = ClueUI.Instance.puzzleParent.GetComponentsInChildren<PuzzleSlot>();
                _aSlots = ClueUI.Instance.answerParent.GetComponentsInChildren<AnswerSlot>();
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
            for (var i = 0; i < _pSlots.Length; i++)
            {
                _pSlots[i].add_symbol(puzzle.puzzleSymbols[i]);
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ClueUI.Instance.reliquaryUI.SetActive(true);
        }

        /// <summary>
        /// Get symbol selected from select symbol ui
        /// </summary>
        /// <param name="index"></param>
        /// <param name="s"></param>
        public void set_answer_symbol(Symbol s)
        {
            for(var i = 0; i < _aSlots.Length; i++)
            {
                if (_aSlots[i].pressed)
                {
                    _aSlots[i].pressed = false;
                    _aSlots[i].AddSymbol(s);
                }
            }
        }


        /// <summary>
        /// On activate button press
        /// </summary>
        public void check_answer()
        {
            for(var i = 0; i != puzzle.answerSymbols.Length; i++)
            {
                if (_aSlots[i] != puzzle.answerSymbols[i])
                {
                    Debug.Log(i + "th symbol wrong.");
                    return;
                }
            }

            Debug.Log("Complete Level");
        }
    }
}
