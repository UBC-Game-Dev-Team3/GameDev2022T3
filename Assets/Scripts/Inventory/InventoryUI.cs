using DialogueStory;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Inventory
{
    /// <summary>
    ///     Script for the inventory's UI.
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        [Tooltip("Parent transform of all inventory slots")]
        public Transform itemsParent;

        /// <remarks>
        /// NOT THE PREFAB
        /// </remarks>
        [Tooltip("Small icon indicating item to display.")] public Image activeItemDisplay;
        /// <summary>
        /// NOT THE PREFAB
        /// </summary>
        [Tooltip("Full Inventory UI")] public GameObject inventoryUI;

        private InventoryManager _inventoryManager;
        private InventorySlot[] _slots;
        private StarterAssetsInputs _input;

        /// <summary>
        ///     On awake, find singleton inventory instance and disable the UI.
        /// </summary>
        private void Awake()
        {
            _input = FindObjectOfType<StarterAssetsInputs>();
            _inventoryManager = InventoryManager.Instance;
            _inventoryManager.onItemChanged.AddListener(UpdateUI);
            inventoryUI.SetActive(false);
            _slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        }

        /// <summary>
        ///     Checks every frame as to whether or not the inventory should be set active.
        /// </summary>
        private void Update()
        {
            if (_input.inventory)
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                activeItemDisplay.enabled = !inventoryUI.activeSelf;
                PlayerRelated.MovementEnabled = !inventoryUI.activeSelf;
                PlayerRelated.InteractionEnabled = !inventoryUI.activeSelf;
                _input.inventory = false;
            }

            if (!inventoryUI.activeInHierarchy) return;
            /*
            if (Input.GetKeyDown(SettingsManager.Instance.inventoryUp))
            {
                _inventoryManager.ChangeSelectedIndex(false);
            }
            if (Input.GetKeyDown(SettingsManager.Instance.inventoryDown))
            {
                _inventoryManager.ChangeSelectedIndex(true);
            }*/
        }

        /// <summary>
        ///     Updates the UI.
        /// </summary>
        public void UpdateUI()
        {
            for (int i = 0; i < _slots.Length; i++)
                if (i < _inventoryManager.items.Count) _slots[i].AddItem(_inventoryManager.items[i]);
                else _slots[i].ClearSlot();
            activeItemDisplay.sprite = _inventoryManager.SelectedItem.icon;
        }
    }
}