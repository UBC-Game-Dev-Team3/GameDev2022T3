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

        [Tooltip("List of Words Parent")]
        public Transform wordsTransform;
        [Tooltip("List of Symbols Parent")]
        public Transform symbolsTransform;
        [Tooltip("List of Button Answers Transform")]
        public Transform wordAnswersTransform;

        [Tooltip("Prefab for Translation Buttons")]
        public GameObject translationButtonPrefab;
        [Tooltip("Prefab for Symbol UI")]
        public GameObject symbolPrefab;
        
        private TranslationPuzzle _puzzle;
        private InputActionMap _actions;
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
            
            Symbol[] words = _puzzle.words.Select(pair => pair.word).ToArray();
            Utilities.InstantiateToLength(translationButtonPrefab,wordsTransform,words.Length);
            for (int i = 0; i < words.Length; i++)
            {
                TranslationSymbolButton button = wordsTransform.GetChild(i).GetComponent<TranslationSymbolButton>();
                button.DisplayedSymbol = words[i];
                button.ui = this;
                button.UpdateFromHint(_puzzle.words[i]);
            }

            UpdateSymbols(0);

            for (int i = 0; i < words.Length; i++)
            {
                //SymbolManager.Instance.SymbolLookup[words[i].symbolName].SeenByPlayer = true;
                words[i].SeenByPlayer = true;
                for (int j = 0; j < words[i].contents.Length; j++)
                {
                    //SymbolManager.Instance.SymbolLookup[words[i].contents[j].symbolName].SeenByPlayer = true;
                    words[i].contents[j].SeenByPlayer = true;
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
        }

        public void OnButtonClick(string symbolName)
        {
            UpdateSymbols(_puzzle.words.Select((pair, index) => new {pair.word.symbolName, index})
                .First(pair => pair.symbolName == symbolName).index);
        }
    }
}
