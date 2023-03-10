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
        
        [Tooltip("Area to place rings")]
        public GameObject ringArea;
        [Tooltip("Prefab of the Ring")]
        public GameObject ringPrefab;

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

        public void OnActivateButtonClick()
        {
            if (_puzzle.solved)
            {
                _puzzle.onSuccess?.Invoke();
                Debug.Log("SUCCESS");
            }
            else
            {
                _puzzle.onFailure?.Invoke();
                Debug.Log("FAILURE");
            }
            CloseUI();
        }
        
        private void CancelUI(InputAction.CallbackContext ctx)
        {
            OnBackButtonClick();
        }

        private void UpdateRings()
        {
            Utilities.InstantiateToLength(ringPrefab, ringArea.transform, _puzzle.rings.Length);
            for (int i = 0; i < _puzzle.rings.Length; i++)
            {
                ReliquaryRing ring = ringArea.transform.GetChild(i).GetComponent<ReliquaryRing>();
                ring.ui = this;
                ring.UpdateRing(_puzzle, i);
            }
        }

        public void OnRingClick(int index)
        {
            int prevIndex = _puzzle.rings[index].SelectedIndex;
            if (prevIndex >= _puzzle.rings[index].options.Length - 1) _puzzle.rings[index].SelectedIndex = 0;
            else _puzzle.rings[index].SelectedIndex++;
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
            UpdateRings();
        }
        
        public void CloseUI()
        {
            PlayerRelated.TriggerUIClose();
            UI.SetActive(false);
            _actions.FindAction("Select").performed -= CancelUI;
        }
    }
}