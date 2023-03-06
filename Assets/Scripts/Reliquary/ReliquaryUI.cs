using UnityEngine;
using UnityEngine.InputSystem;
using Util;

namespace Reliquary
{
    // do i have to spell this properly every time :skull:
    public class ReliquaryUI : MonoBehaviour
    {
        [Tooltip("UI object to enable on open")]
        public GameObject UI;

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

        public void OpenUI(ReliquaryPuzzle puzzle)
        {
            _puzzle = puzzle;
            PlayerRelated.TriggerUIOpen();
            UI.SetActive(true);
            _actions.FindAction("Select").performed += CancelUI;
        }
        
        public void CloseUI()
        {
            PlayerRelated.TriggerUIClose();
            UI.SetActive(false);
            _actions.FindAction("Select").performed -= CancelUI;
        }
    }
}