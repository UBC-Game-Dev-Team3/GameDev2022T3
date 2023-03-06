using System.Linq;
using SymbolBook;
using UnityEngine;
using Util;

namespace TranslationUI
{
    /// <summary>
    /// Attach to an object to be highlighted with translation text
    /// </summary>
    [RequireComponent(typeof(Outline))]
    public class SelectableObject : Interactable
    {
        private Outline _outline;
        [Tooltip("Puzzle")]
        public TranslationPuzzle puzzle;
        [Tooltip("Translation UI to enable")]
        public TranslationScreen translationUI;

        public string TooltipText
        {
            get
            {
                string puzzleName = puzzle.objectName;
                string[] words = puzzle.words.Select(pair => pair.word.PlayerSymbolName).ToArray();
                bool hasUnanswered = words.Any(str => str == Symbol.SymbolDefaultName);
                if (hasUnanswered) return puzzleName + '\n' + Symbol.SymbolDefaultName;
                return puzzleName + '\n' + string.Join(' ', words);
            }
        }
        private void Awake()
        {
            _outline = GetComponent<Outline>();
        }

        public override void Interact(object src, params object[] args)
        {
            base.Interact(src, args);
            translationUI.OpenUI(puzzle);
        }

        public void Select()
        {
            _outline.enabled = true;
        }

        public void Deselect()
        {
            _outline.enabled = false;
        }
    }
}