using SymbolBook;
using UnityEngine;
using UnityEngine.Serialization;

namespace Reliquary
{
    public class SymbolSelectUI : MonoBehaviour
    {
        [SerializeField] private Symbol[] knownSymbols;
        private SelectSlot[] _slots;

        private void Start()
        {
            _slots = ClueUI.Instance.selectParent.GetComponentsInChildren<SelectSlot>();
            FillSymbols();
        }

        private void FillSymbols()
        {
            for(var i = 0; i < _slots.Length; i++)
            {
                _slots[i].add_symbol(knownSymbols[i]);
            }
        }
    }
}
