using DialogueStory;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SymbolBook
{
    public class SymbolBookUI : MonoBehaviour
    {
        public string allSymbolsPath = "Symbols";
        public GameObject ui;

        public TMP_InputField nameField;

        public TMP_InputField description;

        public Image image;
        public bool ignore;
        private StarterAssetsInputs _input;
        private int index = 0;
        private Symbol[] _symbols;
        /// <summary>
        ///     On awake, find singleton inventory instance and disable the UI.
        /// </summary>
        private void Awake()
        {
            _input = FindObjectOfType<StarterAssetsInputs>();
            _symbols = Resources.LoadAll<Symbol>(allSymbolsPath);
            if (ui) ui.SetActive(false);

            ignore = true;
        }

        private void Update()
        {
            if (ignore) return;
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
            _symbols[index].PlayerSymbolName = nameField.text;
            _symbols[index].PlayerNotes = description.text;
        }

        /// <summary>
        ///     Updates the UI.
        /// </summary>
        public void UpdateUI()
        {
            nameField.text = _symbols[index].PlayerSymbolName;
            description.text = _symbols[index].PlayerNotes;
            image.sprite = _symbols[index].image;
        }
        
        public void OnRightClicked()
        {
            SaveToObject();
            if (index == _symbols.Length - 1) index = -1;
            index++;
            UpdateUI();
        }

        public void OnLeftClicked()
        {
            SaveToObject();
            if (index == 0) index = _symbols.Length;
            index--;
            UpdateUI();
        }

    }
}