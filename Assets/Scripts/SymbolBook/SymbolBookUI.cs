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
        public GameObject scroll;
        public GameObject scrollContent;
        public GameObject scrollPrefab;
        
        public TMP_InputField nameField;

        public TMP_InputField description;

        public Image image;
        public GridLayoutGroup appearedIn;
        public GameObject gridPrefab;
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
            if (ui)
            {
                ui.SetActive(false);
                scroll.SetActive(false);
            }
        }

        private void Update()
        {
            if (!_input.symbolBook) return;
            bool isDisplayed = ui.activeSelf;
            _input.symbolBook = false;
            if ((!isDisplayed && !PlayerRelated.ShouldListenForUIOpenEvents) || (nameField.isFocused || description.isFocused)) return;
            ui.SetActive(!isDisplayed);
            scroll.SetActive(!isDisplayed);
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
            int scrollChild = scrollContent.transform.childCount;
            Symbol[] scrollSymbols = _manager.SeenSymbols();
            int desiredScrollChild = scrollSymbols.Length;
            for (int i = scrollChild-1; i >= desiredScrollChild; i--)
            {
                Destroy(scrollContent.transform.GetChild(i).gameObject);
            }

            for (int i = scrollChild; i < desiredScrollChild; i++)
            {
                Instantiate(scrollPrefab, scrollContent.transform);
            }

            for (int i = 0; i < desiredScrollChild; i++)
            {
                SymbolBookButton button = scrollContent.transform.GetChild(i).GetComponent<SymbolBookButton>();
                button.DisplayedSymbol = scrollSymbols[i];
                button.ui = this;
            }

            Symbol symbol = _manager.symbols[index];
            nameField.text = symbol.PlayerSymbolName;
            description.text = symbol.PlayerNotes;
            symbol.Render(image.transform.gameObject,250, false);
            int children = appearedIn.transform.childCount;
            Symbol[] parents = _manager.SeenParents(symbol);
            int desiredChildren = parents.Length;
            if (desiredChildren > children)
            {
                Debug.LogError("Too many children: got " + desiredChildren + ", had " + children);
            }
            for (int i = 0; i < desiredChildren; i++)
            {
                SymbolBookButton button = appearedIn.transform.GetChild(i).GetComponent<SymbolBookButton>();
                button.gameObject.SetActive(true);
                button.DisplayedSymbol = parents[i];
                button.ui = this;
            }

            for (int i = desiredChildren; i < children; i++)
            {
                appearedIn.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public void OnButtonClick(string newSymbolName)
        {
            SaveToObject();
            index = _manager.SymbolIndex(newSymbolName);
            UpdateUI();
        }
    }
}