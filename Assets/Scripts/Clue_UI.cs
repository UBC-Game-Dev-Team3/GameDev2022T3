using SymbolBook;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class Clue_UI : MonoBehaviour
{
    #region Singleton
    public static Clue_UI instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("One clue_ui instance already exists");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject clue_ui;
    public GameObject reliquary_ui;
    public GameObject symbolselect_ui;
    public Transform symbol_parent;
    public Transform puzzle_parent;
    public Transform answer_parent;
    public Transform select_parent;

    /// <summary>
    /// On close button close
    /// </summary>
    public void close()
    {
        clue_ui.SetActive(false);
        reliquary_ui.SetActive(false);
        symbolselect_ui.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
    }

    /// <summary>
    /// On record button press
    /// </summary>
    public void add_notes()
    {
        Clue_object[] clue_objects = FindObjectsOfType<Clue_object>();
        int index = 0;
        bool selected = false;
        for(var i = 0; i != clue_objects.Length; i++)
        {
            if (clue_objects[i].selected)
            {
                index = i;
                break;
            }
        }


        for(var i = 0; i != clue_objects[index].slots.Length; i++) 
        {
            if (clue_objects[index].slots[i].selected)
            {
                selected = true;
                if(clue_objects[index].clue.symbols[i] != null)
                {
                    clue_objects[index].clue.symbols[i].PlayerSymbolName = clue_objects[index].slots[i].get_name();
                    clue_objects[index].clue.symbols[i].PlayerNotes = clue_objects[index].slots[i].get_notes();
                } else
                {
                    Debug.LogWarning("The symbol selected in clue panel does not exits.");
                }
                
            }
        }

        if(!selected)
        {
            clue_objects[index].slots[0].show_warning();
        } else
        {
            clue_objects[index].slots[0].show_added();
        }
    }

}
