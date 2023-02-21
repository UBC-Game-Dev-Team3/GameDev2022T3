using StarterAssets;
using TMPro;
using UnityEngine;
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
            PlayerRelated.TriggerUIClose();
            UI.SetActive(false);
        }

        public void OpenUI()
        {
            PlayerRelated.TriggerUIOpen();
            UI.SetActive(true);
        }
    }
}
