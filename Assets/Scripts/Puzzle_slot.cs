using SymbolBook;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_slot : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void add_symbol(Symbol s)
    {
        icon.sprite = s.image;
    }
}
