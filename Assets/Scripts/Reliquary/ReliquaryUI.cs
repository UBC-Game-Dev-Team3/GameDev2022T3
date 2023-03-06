using SymbolBook;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Util;

namespace Reliquary
{
    // do i have to spell this properly every time :skull:
    public class ReliquaryUI : MonoBehaviour
    {
        [Tooltip("UI object to enable on open")]
        public GameObject UI;
        [Tooltip("Symbol Prefab (non-button)")]
        public GameObject symbolPrefab;
        
        [Tooltip("Location to place 'Seal Symbols'")]
        public GameObject centerSymbolLocation;

        private InputActionMap _actions;
        private ReliquaryPuzzle _puzzle;
        
        private void Awake()
        {
            _actions = FindObjectOfType<PlayerInput>().actions.FindActionMap("Player");
        }

        public void OnBackButtonClick()
        {
            CloseUI();
        }
        
        private void CancelUI(InputAction.CallbackContext ctx)
        {
            OnBackButtonClick();
        }

        private void UpdateCenterSymbols()
        {
            Symbol[] symbols = _puzzle.seal;
            int symbolCount = symbols.Length;
            Utilities.InstantiateToLength(symbolPrefab,centerSymbolLocation.transform,symbolCount);
            for (int i = 0; i < symbolCount; i++)
            {
                Transform child = centerSymbolLocation.transform.GetChild(i);
                Image img = child.GetComponentInChildren<Image>();
                img.preserveAspect = true;  
                symbols[i].Render(img.gameObject);
                TMP_Text text = child.GetComponentInChildren<TMP_Text>();
                text.text = symbols[i].PlayerSymbolName;
            }
        }

        public void OpenUI(ReliquaryPuzzle puzzle)
        {
            _puzzle = puzzle;
            PlayerRelated.TriggerUIOpen();
            UI.SetActive(true);
            _actions.FindAction("Select").performed += CancelUI;
            
            UpdateCenterSymbols();
        }
        
        public void CloseUI()
        {
            PlayerRelated.TriggerUIClose();
            UI.SetActive(false);
            _actions.FindAction("Select").performed -= CancelUI;
        }
    }
}