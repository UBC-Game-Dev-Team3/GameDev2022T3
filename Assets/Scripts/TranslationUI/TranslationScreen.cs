using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;

namespace TranslationUI
{
    public class TranslationScreen : MonoBehaviour
    {
        /// <summary>
        /// Gets/sets inscription flavor name text
        /// </summary>
        public string InscriptionFlavorName
        {
            get => InscriptionFlavorText.text;
            set => InscriptionFlavorText.text = value;
        }
        [Tooltip("Inscription Flavor Text object")]
        public TMP_Text InscriptionFlavorText;
        [Tooltip("UI object to enable on open")]
        public GameObject UI;
        [Tooltip("Tooltip Object UI")]
        public GameObject tooltipUI;

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
        }
    }
}
