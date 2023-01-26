using SymbolBook;
using UnityEngine;
using UnityEngine.UI;

namespace Reliquary
{
    public class PuzzleSlot : MonoBehaviour
    {
        [SerializeField] private Image icon;

        public void add_symbol(Symbol s)
        {
            icon.sprite = s.image;
        }
    }
}
