using SymbolBook;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle Symbol", menuName = "Puzzle")]
public class Puzzle : ScriptableObject
{
    [Tooltip("Symbols used for Reliquary Puzzle")]
    public Symbol[] puzzle_symbols;
    [Tooltip("Answers to be compared with user input")]
    public Symbol[] answer_symbols;
}
