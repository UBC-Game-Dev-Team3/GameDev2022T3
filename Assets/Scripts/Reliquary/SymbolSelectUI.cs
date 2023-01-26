using SymbolBook;
using UnityEngine;
using UnityEngine.Serialization;

namespace Reliquary
{
    public class SymbolSelectUI : MonoBehaviour
    {

        [FormerlySerializedAs("known_symbols")] [SerializeField] private Symbol[] knownSymbols;
        private SelectSlot[] _slots;

        private void Start()
        {
            _slots = ClueUI.Instance.selectParent.GetComponentsInChildren<SelectSlot>();
            fill_symbols();
        }

        public void fill_symbols()
        {
            for(var i = 0; i < _slots.Length; i++)
            {
                _slots[i].add_symbol(knownSymbols[i]);
            }
        }
    }
}
