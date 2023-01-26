using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Reliquary
{
    public class ClueUI : MonoBehaviour
    {
        #region Singleton
        public static ClueUI Instance;
        private void Awake()
        {
            if(Instance != null)
            {
                Debug.LogWarning("One clue_ui instance already exists");
                return;
            }
            Instance = this;
        }
        #endregion

        [FormerlySerializedAs("clue_ui")] public GameObject clueUI;
        [FormerlySerializedAs("reliquary_ui")] public GameObject reliquaryUI;
        [FormerlySerializedAs("symbolselect_ui")] public GameObject symbolselectUI;
        [FormerlySerializedAs("symbol_parent")] public Transform symbolParent;
        [FormerlySerializedAs("puzzle_parent")] public Transform puzzleParent;
        [FormerlySerializedAs("answer_parent")] public Transform answerParent;
        [FormerlySerializedAs("select_parent")] public Transform selectParent;

        /// <summary>
        /// On close button close
        /// </summary>
        public void Close()
        {
            clueUI.SetActive(false);
            reliquaryUI.SetActive(false);
            symbolselectUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible= false;
        }

        /// <summary>
        /// On record button press
        /// </summary>
        public void add_notes()
        {
            ClueObject[] clueObjects = FindObjectsOfType<ClueObject>();
            int index = 0;
            bool selected = false;
            for(var i = 0; i != clueObjects.Length; i++)
            {
                if (clueObjects[i].selected)
                {
                    index = i;
                    break;
                }
            }


            for(var i = 0; i != clueObjects[index].slots.Length; i++) 
            {
                if (clueObjects[index].slots[i].selected)
                {
                    selected = true;
                    if(clueObjects[index].clue.symbols[i] != null)
                    {
                        clueObjects[index].clue.symbols[i].PlayerSymbolName = clueObjects[index].slots[i].get_name();
                        clueObjects[index].clue.symbols[i].PlayerNotes = clueObjects[index].slots[i].get_notes();
                    } else
                    {
                        Debug.LogWarning("The symbol selected in clue panel does not exits.");
                    }
                
                }
            }

            if(!selected)
            {
                clueObjects[index].slots[0].show_warning();
            } else
            {
                clueObjects[index].slots[0].show_added();
            }
        }

    }
}
