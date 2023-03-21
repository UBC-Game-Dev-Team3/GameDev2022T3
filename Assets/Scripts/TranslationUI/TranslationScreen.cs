using System.Linq;
using SymbolBook;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Util;

namespace TranslationUI
{
    public class TranslationScreen : MonoBehaviour
    {
        [Tooltip("Inscription Flavor Text object")]
        public TMP_Text InscriptionFlavorText;
        [Tooltip("UI object to enable on open")]
        public GameObject UI;
        [Tooltip("Tooltip Object UI")]
        public GameObject tooltipUI;
        [Tooltip("Confirm button")]
        public Button confirmButton;

        [Tooltip("List of Words Parent")]
        public Transform wordsTransform;
        [Tooltip("List of Symbols Parent")]
        public Transform symbolsTransform;
        [Tooltip("List of Button Answers Transform")]
        public Transform wordAnswersTransform;

        [Tooltip("Prefab for Symbol UI Buttons")]
        public GameObject translationButtonPrefab;
        [Tooltip("Prefab for Symbol UI")]
        public GameObject symbolPrefab;
        [Tooltip("Prefab for Options")]
        public GameObject optionButtonPrefab;
        
        private TranslationPuzzle _puzzle;
        private InputActionMap _actions;
        private int _currIndex = -1;
        private void Awake()
        {
            _actions = FindObjectOfType<PlayerInput>().actions.FindActionMap("Player");
            PlayerRelated.OnUIChangeDelegate += OnUIStateChange;
        }
        
        private void CancelUI(InputAction.CallbackContext ctx)
        {
            OnCancelClick();
        }

        public void OnUIStateChange(bool newVisibility)
        {
            tooltipUI.SetActive(!newVisibility);
        }

        public void OnCancelClick()
        {
            CloseUI();
        }

        public void OnConfirmClick()
        {
            CloseUI();
        }

        public void CloseUI()
        {
            PlayerRelated.TriggerUIClose();
            UI.SetActive(false);
            _actions.FindAction("Select").performed -= CancelUI;
        }

        public void OpenUI(TranslationPuzzle puzzle)
        {
            _puzzle = puzzle;
            PlayerRelated.TriggerUIOpen();
            UI.SetActive(true);
            _actions.FindAction("Select").performed += CancelUI;
            InscriptionFlavorText.text = _puzzle.objectName;
            for (int i = 0; i < _puzzle.words.Length; i++)
            {
                if (_puzzle.words[i].startKnown)
                    _puzzle.words[i].word.PlayerSymbolName = _puzzle.words[i].hint;
            }
            
            Symbol[] words = _puzzle.words.Select(pair => pair.word).ToArray();
            Utilities.InstantiateToLength(translationButtonPrefab,wordsTransform,words.Length);
            for (int i = 0; i < words.Length; i++)
            {
                UpdateWord(i);
            }

            UpdateSymbols(0);
            UpdateButtons();

            foreach (Symbol symbol in words)
            {
                symbol.SeenByPlayer = true;
                foreach (Symbol child in symbol.contents)
                {
                    child.SeenByPlayer = true;
                }
            }
        }

        private void UpdateSymbols(int index)
        {
            Symbol wordInQuestion = _puzzle.words[index].word;
            int symbolCount = wordInQuestion.contents.Length;
            Utilities.InstantiateToLength(symbolPrefab,symbolsTransform,symbolCount);
            for (int i = 0; i < symbolCount; i++)
            {
                Transform child = symbolsTransform.GetChild(i);
                Image img = child.GetComponentInChildren<Image>();
                img.preserveAspect = true;  
                wordInQuestion.contents[i].Render(img.gameObject);
                TMP_Text text = child.GetComponentInChildren<TMP_Text>();
                text.text = wordInQuestion.contents[i].PlayerSymbolName;
            }

            if (_currIndex != -1)
            {
                TranslationSymbolButton prevButton =
                    wordsTransform.GetChild(_currIndex).GetComponent<TranslationSymbolButton>();
                prevButton.DeHighlight();
            }

            _currIndex = index;
            TranslationSymbolButton button = wordsTransform.GetChild(index).GetComponent<TranslationSymbolButton>();
            button.Highlight();
        }

        private void UpdateButtons()
        {
            if (_puzzle.words[_currIndex].startKnown)
            {
                Utilities.InstantiateToLength(optionButtonPrefab, wordAnswersTransform, 0);
                return;
            }
            
            string[] options = _puzzle.words[_currIndex].options;
            Utilities.InstantiateToLength(optionButtonPrefab,wordAnswersTransform,options.Length);
            for (int i = 0; i < options.Length; i++)
            {
                TranslationSelectionButton child = wordAnswersTransform.GetChild(i)
                    .GetComponent<TranslationSelectionButton>();
                child.ui = this;
                child.Initialize(_puzzle.words[_currIndex], i);
            }

            for (int i = 0; i < _puzzle.words.Length; i++)
            {
                if (_puzzle.words[i].word.PlayerSymbolName == Symbol.SymbolDefaultName || string.IsNullOrWhiteSpace(_puzzle.words[i].word.PlayerSymbolName))
                {
                    confirmButton.interactable = false;
                    return;
                }
            }

            confirmButton.interactable = true;
        }

        private void UpdateWord(int index)
        {
            TranslationSymbolButton button = wordsTransform.GetChild(index).GetComponent<TranslationSymbolButton>();
            button.DisplayedSymbol = _puzzle.words[index].word;
            button.ui = this;
            button.UpdateFromHint(_puzzle.words[index]);
        }
        
        public void OnSymbolClick(string symbolName)
        {
            UpdateSymbols(_puzzle.words.Select((pair, index) => new {pair.word.symbolName, index})
                .First(pair => pair.symbolName == symbolName).index);
            UpdateButtons();
        }

        public void OnButtonClick(int index)
        {
            _puzzle.words[_currIndex].word.PlayerSymbolName = _puzzle.words[_currIndex].options[index];
            UpdateButtons();
            UpdateWord(_currIndex);
        }
    }
}
