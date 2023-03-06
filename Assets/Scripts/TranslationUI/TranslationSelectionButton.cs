using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TranslationUI
{
    public class TranslationSelectionButton : MonoBehaviour
    {
        [NonSerialized]
        public TranslationScreen ui;

        public Button button;
        public TMP_Text text;
        public GameObject highlight;
        
        public int Index { get; set; }

        public void Initialize(TranslationPuzzle.WordPair puzzleWord, int index)
        {
            Index = index;
            highlight.SetActive(puzzleWord.hint == puzzleWord.options[index]);
            text.text = puzzleWord.options[index];
            button.interactable = puzzleWord.word.PlayerSymbolName != puzzleWord.options[index];
        }

        public void OnClick()
        {
            ui.OnButtonClick(Index);
        }
    }
}