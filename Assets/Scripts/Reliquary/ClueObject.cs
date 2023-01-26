using DialogueStory;
using UnityEngine;
using Util;

namespace Reliquary
{
    public class ClueObject : Interactable
    {
        public Clue clue;
        public SymbolSlot[] slots;
        public bool selected;
        private void Start()
        {
            if (ClueUI.Instance != null)
            {
                slots = ClueUI.Instance.symbolParent.GetComponentsInChildren<SymbolSlot>();
                Debug.Log("Test");
            }

            selected= false;
        }

        /// <summary>
        /// Fills in slots in clue panel
        /// </summary>
        private void OpenPanel()
        {
            for(var i = 0; i < slots.Length; i++)
            {
                slots[i].add_symbol(clue.symbols[i]);
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PlayerRelated.ShouldListenForUIOpenEvents = false;
            ClueUI.Instance.clueUI.SetActive(true);
            selected = true;
        }

        public override void Interact(object src, params object[] args)
        {
            base.Interact(src, args);
            OpenPanel();
        }
  
    }
}
