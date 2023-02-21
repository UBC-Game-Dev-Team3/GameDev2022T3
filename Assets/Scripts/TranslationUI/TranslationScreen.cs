using System.Linq;
using SymbolBook;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
            }
        }

        public void OnButtonClick(string displayedSymbolSymbolName)
        {
            
        }
    }
}
