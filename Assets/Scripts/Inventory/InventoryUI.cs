using DialogueStory;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    /// <summary>
    ///     Script for the inventory's UI.
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        /// <remarks>
        /// NOT THE PREFAB
        /// </remarks>
        [Tooltip("Small icon indicating item to display.")] public Image activeItemDisplay;
        /// <summary>
        /// NOT THE PREFAB
        /// </summary>
        [Tooltip("Full Inventory UI")] public GameObject inventoryUI;

        [Tooltip("Title text")] public TextMeshProUGUI title;
        [Tooltip("Description text")] public TextMeshProUGUI desc;

        [Tooltip("Parent for the 3D object to render")]
        public GameObject parentFor3D;
        
        [Tooltip("Left Icon")]public Image leftIcon;
        [Tooltip("Middle Icon")]public Image middleIcon;
        [Tooltip("Right Icon")]public Image rightIcon;
        public Item test;
        
        
        private InventoryManager _inventoryManager;
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
            _inventoryManager.Add(test);
            _inventoryManager.ChangeSelectedIndex(true);
            leftIcon.preserveAspect = true;
            middleIcon.preserveAspect = true;
            rightIcon.preserveAspect = true;
            UpdateUI();
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
                Cursor.lockState = activeItemDisplay.enabled ? CursorLockMode.Locked : CursorLockMode.None;
                _input.inventory = false;
                UpdateUI();
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
            middleIcon.sprite = _inventoryManager.SelectedItem?.icon;
            title.text = _inventoryManager.SelectedItem?.name;
            desc.text = _inventoryManager.SelectedItem?.description;
            leftIcon.sprite = _inventoryManager.LeftSelectedItem?.icon;
            rightIcon.sprite = _inventoryManager.RightSelectedItem?.icon;
            activeItemDisplay.sprite = _inventoryManager.SelectedItem?.icon;
            if (activeItemDisplay.enabled)
            {
                if (parentFor3D.transform.childCount > 0)
                {
                    Destroy(parentFor3D.transform.GetChild(0));
                }
            }
            else
            {
                GameObject go = Instantiate(_inventoryManager.SelectedItem?.prefab, parentFor3D.transform);
                SetLayerRecursively(go, LayerMask.NameToLayer("UI"));
            }
        }

        private static void SetLayerRecursively(GameObject obj, int layer)
        {
            if (null == obj) return;
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                if (null == child) continue;
                SetLayerRecursively(child.gameObject, layer);
            }
        }
    }
}