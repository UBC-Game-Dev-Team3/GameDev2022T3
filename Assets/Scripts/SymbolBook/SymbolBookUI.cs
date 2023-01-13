using DialogueStory;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SymbolBook
{
    public class SymbolBookUI : MonoBehaviour
    {
        public GameObject ui;

        public TMP_InputField nameField;

        public TMP_InputField description;

        public Image image;
        private StarterAssetsInputs _input;
        private int index = 0;
        private SymbolManager _manager;
        /// <summary>
        ///     On awake, find singleton inventory instance and disable the UI.
        /// </summary>
        private void Awake()
        {
            _input = FindObjectOfType<StarterAssetsInputs>();
            _manager = SymbolManager.Instance;
            if (ui) ui.SetActive(false);
        }

        private void Update()
        {
            if (!_input.symbolBook) return;
            bool isDisplayed = ui.activeSelf;
            _input.symbolBook = false;
            if ((!isDisplayed && !PlayerRelated.ShouldListenForUIOpenEvents) || (nameField.isFocused || description.isFocused)) return;
            ui.SetActive(!isDisplayed);
            PlayerRelated.MovementEnabled = isDisplayed;
            PlayerRelated.InteractionEnabled = isDisplayed;
            Cursor.lockState = isDisplayed ? CursorLockMode.Locked : CursorLockMode.None;
            SaveToObject();
            UpdateUI();
            PlayerRelated.ShouldListenForUIOpenEvents = isDisplayed;
        }

        private void SaveToObject()
        {
            _manager.symbols[index].PlayerSymbolName = nameField.text;
            _manager.symbols[index].PlayerNotes = description.text;
        }

        /// <summary>
        ///     Updates the UI.
        /// </summary>
        public void UpdateUI()
        {
            nameField.text = _manager.symbols[index].PlayerSymbolName;
            description.text = _manager.symbols[index].PlayerNotes;
            image.sprite = _manager.symbols[index].image;
        }
        
        public void OnRightClicked()
        {
            SaveToObject();
            if (index == _manager.symbols.Length - 1) index = -1;
            index++;
            UpdateUI();
        }

        public void OnLeftClicked()
        {
            SaveToObject();
            if (index == 0) index = _manager.symbols.Length;
            index--;
            UpdateUI();
        }
    }
}