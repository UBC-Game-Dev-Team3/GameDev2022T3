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
            ui?.SetActive(false);
        }

        private void Update()
        {
            if (_input.symbolBook)
            {
                bool isDisplayed = ui.activeSelf;
                ui.SetActive(!isDisplayed);
                PlayerRelated.MovementEnabled = isDisplayed;
                PlayerRelated.InteractionEnabled = isDisplayed;
                Cursor.lockState = isDisplayed ? CursorLockMode.Locked : CursorLockMode.None;
                _input.symbolBook = false;
                SaveToObject();
                UpdateUI();
            }
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