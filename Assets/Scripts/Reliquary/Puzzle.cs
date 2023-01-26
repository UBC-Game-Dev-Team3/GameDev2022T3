using SymbolBook;
using UnityEngine;
using UnityEngine.Serialization;

namespace Reliquary
{
    [CreateAssetMenu(fileName = "Puzzle Symbol", menuName = "Puzzle")]
    public class Puzzle : ScriptableObject
    {
        [FormerlySerializedAs("puzzle_symbols")] [Tooltip("Symbols used for Reliquary Puzzle")]
        public Symbol[] puzzleSymbols;
        [FormerlySerializedAs("answer_symbols")] [Tooltip("Answers to be compared with user input")]
        public Symbol[] answerSymbols;
    }
}
