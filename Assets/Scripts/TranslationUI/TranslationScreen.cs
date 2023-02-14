using System;
using DialogueStory;
using StarterAssets;
using TMPro;
using UnityEngine;

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
        private StarterAssetsInputs _input;
        private void Awake()
        {
            _input = FindObjectOfType<StarterAssetsInputs>();
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
            PlayerRelated.MovementEnabled = true;
            PlayerRelated.InteractionEnabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            PlayerRelated.ShouldListenForUIOpenEvents = true;
            UI.SetActive(false);
        }

        public void OpenUI()
        {
            PlayerRelated.MovementEnabled = false;
            PlayerRelated.InteractionEnabled = false;
            Cursor.lockState = CursorLockMode.None;
            PlayerRelated.ShouldListenForUIOpenEvents = false;
            UI.SetActive(true);
        }
    }
}
