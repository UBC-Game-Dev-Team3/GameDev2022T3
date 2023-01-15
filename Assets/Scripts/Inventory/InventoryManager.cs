using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    /// <summary>
    ///     Script for having an inventory attached to a player.
    /// </summary>
    [DisallowMultipleComponent]
    public class InventoryManager : MonoBehaviour
    {
        
        #region Singleton Design Pattern

        /// <summary>
        ///     Singleton instance.
        /// </summary>
        private static InventoryManager _privateStaticInstance;

        /// <summary>
        ///     Getter for the singleton.
        /// </summary>
        public static InventoryManager Instance
        {
            get
            {
                if (_privateStaticInstance == null) _privateStaticInstance = FindObjectOfType<InventoryManager>();
                if (_privateStaticInstance != null) return _privateStaticInstance;
                GameObject go = new GameObject("Inventory Manager");
                _privateStaticInstance = go.AddComponent<InventoryManager>();
                return _privateStaticInstance;
            }
        }

        #endregion
        
        [Tooltip("Runs on item added/removed.")]
        public UnityEvent onItemChanged = new UnityEvent();

        [Tooltip("List of items in inventory.")]
        public List<Item> items = new List<Item>();

        public int indexOfSelection { get; private set; } = -1;
        
        internal Item LeftSelectedItem
        {
            get
            {
                if (indexOfSelection < 0 || indexOfSelection >= items.Count) return null;
                if (indexOfSelection == 0) return items[items.Count - 1];
                return items[indexOfSelection-1];
            }
        }

        internal Item SelectedItem
        {
            get
            {
                if (indexOfSelection < 0 || indexOfSelection >= items.Count) return null;
                return items[indexOfSelection];
            }
        }
        internal Item RightSelectedItem
        {
            get
            {
                if (indexOfSelection < 0 || indexOfSelection >= items.Count) return null;
                if (indexOfSelection == items.Count-1) return items[0];
                return items[indexOfSelection+1];
            }
        }

        public void Deequip()
        {
            indexOfSelection = -1; // prayge
        }

        public void ChangeSelectedIndex(bool increment)
        {
            int oldIndex = indexOfSelection;
            if (increment)
            {
                int newIndex = oldIndex + 1;
                if (newIndex >= items.Count) newIndex = 0;
                indexOfSelection = newIndex;
            }
            else
            {
                int newIndex = oldIndex - 1;
                if (newIndex < 0) newIndex = items.Count - 1;
                indexOfSelection = newIndex;
            }

            onItemChanged.Invoke();
        }

        /// <summary>
        ///     On awake, set this to private static instance
        /// </summary>
        private void Awake()
        {
            if (_privateStaticInstance != null && _privateStaticInstance != this)
                Debug.LogWarning("More than one instance of Inventory found!");
            else _privateStaticInstance = this;
        }

        /// <summary>
        ///     Adds an item to the inventory, if space allows.
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>Whether or not the item was successfully added</returns>
        public void Add(Item item)
        {
            if (item == null) return;

            items.Add(item);
            onItemChanged.Invoke();
        }

        /// <summary>
        ///     Removes the item from the inventory.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        public void Remove(Item item)
        {
            items.Remove(item);
            onItemChanged.Invoke();
        }

        /// <summary>
        /// Determines whether the item is currently being active/held in the inventory
        /// </summary>
        /// <param name="item">Item in question</param>
        /// <returns>Whether the item is active or not</returns>
        public bool HasActiveItem(Item item)
        {
            if (indexOfSelection < 0 || indexOfSelection >= items.Count) return false;
            if (item == null) return false;
            return items[indexOfSelection].name == item.name;
        }
    }
}