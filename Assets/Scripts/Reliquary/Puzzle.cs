using SymbolBook;
using UnityEngine;

namespace Reliquary
{
    [CreateAssetMenu(fileName = "Puzzle Symbol", menuName = "Puzzle")]
    public class Puzzle : ScriptableObject
    {
        [Tooltip("Symbols used for Reliquary Puzzle")]
        public Symbol[] puzzleSymbols;
        [Tooltip("Answers to be compared with user input")]
        public Symbol[] answerSymbols;
    }
}
