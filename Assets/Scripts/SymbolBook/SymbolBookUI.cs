using System;
using System.Linq;
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
            UpdateHighlight(-1);
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
            UpdateScroll();
            UpdateMain();
            UpdateSeenInGrid();
        }

        private void UpdateScroll()
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
        }

        private void UpdateMain()
        {
            Symbol symbol = _manager.symbols[_index];
            nameField.text = symbol.HasPlayerModified ? symbol.PlayerSymbolName : "";
            description.text = symbol.PlayerNotes;
            symbol.Render(image.transform.gameObject,250, false);
        }

        private void UpdateSeenInGrid()
        {
            Symbol symbol = _manager.symbols[_index];
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

        private bool modifyingUIState = false;

        public void OnButtonClick(string newSymbolName)
        {
            SaveToObject();
            int prev = _index;
            _index = _manager.SymbolIndex(newSymbolName);
            modifyingUIState = true;
            UpdateUI();
            UpdateHighlight(prev);
            modifyingUIState = false;
        }

        public void OnValueChanged()
        {
            if (modifyingUIState) return;
            SaveToObject();
            _manager.symbols[_index].HasPlayerModified = true;
            UpdateSeenInGrid();
        }

        private void UpdateHighlight(int prevIndex)
        {
            if (prevIndex >= 0)
            {
                int prevScrollIndex =
                    Array.IndexOf(_manager.SeenSymbols().Where(s => !s.isWord).Select(sym => sym.symbolName).ToArray(),
                        _manager.symbols[prevIndex].symbolName);
                if (prevScrollIndex >= 0)
                    scrollContent.transform.GetChild(prevScrollIndex).GetComponent<SymbolBookButton>().Highlighted =
                        false;
            }

            int newIndex =
                Array.IndexOf(_manager.SeenSymbols().Where(s => !s.isWord).Select(sym => sym.symbolName).ToArray(),
                    _manager.symbols[_index].symbolName);
            if (newIndex >= 0)
                scrollContent.transform.GetChild(newIndex).GetComponent<SymbolBookButton>().Highlighted = true;
        }
    }
}