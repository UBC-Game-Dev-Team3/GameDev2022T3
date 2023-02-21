using System.Linq;
using DialogueStory;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Util;

namespace SymbolBook
{
    public class SymbolBookUI : MonoBehaviour
    {
        public GameObject ui;
        public GameObject scroll;
        public GameObject scrollContent;
        public GameObject scrollPrefab;
        
        public TMP_InputField nameField;

        public TMP_InputField description;

        public Image image;
        public GridLayoutGroup appearedIn;
        public GameObject gridPrefab;
        private InputActionMap _actions;
        private int _index;
        private SymbolManager _manager;
        /// <summary>
        ///     On awake, find singleton inventory instance and disable the UI.
        /// </summary>
        private void Awake()
        {
            _actions = FindObjectOfType<PlayerInput>().actions.FindActionMap("Player");
            _actions.FindAction("SymbolBook").performed += OnSymbolBookButtonPress;
            _manager = SymbolManager.Instance;
            if (ui)
            {
                ui.SetActive(false);
                scroll.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            _actions.FindAction("SymbolBook").performed -= OnSymbolBookButtonPress;
        }

        private void OnSymbolBookButtonPress(InputAction.CallbackContext ctx)
        {
            bool isDisplayed = ui.activeSelf;
            if ((!isDisplayed && !PlayerRelated.ShouldListenForUIOpenEvents) || (nameField.isFocused || description.isFocused)) return;
            if (isDisplayed) PlayerRelated.TriggerUIClose();
            else PlayerRelated.TriggerUIOpen();
            ui.SetActive(!isDisplayed);
            scroll.SetActive(!isDisplayed);
            SaveToObject();
            UpdateUI();
        }

        private void Start()
        {
            Symbol[] scrollSymbols = _manager.SeenSymbols().Where(s => !s.isWord).ToArray();
            _index = _manager.SymbolIndex(scrollSymbols[0].symbolName);
        }

        private void SaveToObject()
        {
            _manager.symbols[_index].PlayerSymbolName = nameField.text;
            _manager.symbols[_index].PlayerNotes = description.text;
        }

        /// <summary>
        ///     Updates the UI.
        /// </summary>
        public void UpdateUI()
        {
            Symbol[] scrollSymbols = _manager.SeenSymbols().Where(s => !s.isWord).ToArray();
            int desiredScrollChild = scrollSymbols.Length;
            
            Utilities.InstantiateToLength(scrollPrefab, scrollContent.transform, desiredScrollChild);

            for (int i = 0; i < desiredScrollChild; i++)
            {
                SymbolBookButton button = scrollContent.transform.GetChild(i).GetComponent<SymbolBookButton>();
                button.DisplayedSymbol = scrollSymbols[i];
                button.ui = this;
            }

            Symbol symbol = _manager.symbols[_index];
            nameField.text = symbol.PlayerSymbolName;
            description.text = symbol.PlayerNotes;
            symbol.Render(image.transform.gameObject,250, false);
            Symbol[] parents = _manager.SeenParents(symbol);
            int desiredChildren = parents.Length;
            Utilities.InstantiateToLength(gridPrefab, appearedIn.transform, desiredChildren);
            
            for (int i = 0; i < desiredChildren; i++)
            {
                SymbolBookButton button = appearedIn.transform.GetChild(i).GetComponent<SymbolBookButton>();
                button.gameObject.SetActive(true);
                button.DisplayedSymbol = parents[i];
                button.ui = this;
            }
        }

        public void OnButtonClick(string newSymbolName)
        {
            SaveToObject();
            _index = _manager.SymbolIndex(newSymbolName);
            UpdateUI();
        }
    }
}