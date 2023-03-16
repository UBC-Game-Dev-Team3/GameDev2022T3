using System.Linq;
using SymbolBook;
using UnityEngine;
using Util;

namespace TranslationUI
{
    [RequireComponent(typeof(Outline))]
    public class TranslationTriggerObject : SelectableObject
    {
        [Tooltip("Puzzle")]
        public TranslationPuzzle puzzle;
        [Tooltip("Translation UI to enable")]
        public TranslationScreen translationUI;
        
        public override string TooltipText
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
        
        public override void Interact(object src, params object[] args)
        {
            base.Interact(src, args);
            translationUI.OpenUI(puzzle);
        }
    }
}