using SymbolBook;
using UnityEngine;

/*
 * Contains the symbols to be displayed for each clue object 
 */
namespace Reliquary
{
    [CreateAssetMenu(fileName = "New Clue", menuName = "Clue")]
    public class Clue : ScriptableObject
    {
        [Tooltip("Symbols to be displayed once a clue object is selected")]
        public Symbol[] symbols;
    }
}
