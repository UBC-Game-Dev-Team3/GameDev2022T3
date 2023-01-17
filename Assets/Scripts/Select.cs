using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;

namespace StarterAssets
{
    [RequireComponent(typeof(NewStarterAssets))]
    public class Select : MonoBehaviour
    {
        public Camera cm;
        public TextMeshProUGUI item_name;

        public float view_range = 15f;

        private GameObject selected_game_object;
        //private StarterAssetsInputs input;
        NewStarterAssets inputActions;
        private void Awake()
        {
            Cursor.visible = false;
            inputActions= new NewStarterAssets();
            inputActions.Player.Select.performed += ctx => select_object();
        }

        private void OnEnable()
        {
            inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            inputActions.Player.Disable();
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit_info;

            if (Physics.Raycast(cm.transform.position, cm.transform.forward, out hit_info, view_range))
            {
                if ((hit_info.collider.gameObject.GetComponent<Interactable>()))
                {
                    //Debug.Log(hit_info.collider.name);
                    selected_game_object = hit_info.collider.gameObject;
                    selected_game_object.GetComponent<Outline>().enabled = true;

                    item_name.text = hit_info.collider.gameObject.name; //display item name
                }
            }
            else if (selected_game_object != null)
            {
                selected_game_object.GetComponent<Outline>().enabled = false;
                selected_game_object = null;
                item_name.text = string.Empty;
            }

        }

        /// <summary>
        /// On left button mouse click
        /// </summary>
        void select_object()
        {
            if (selected_game_object)
            {
                Debug.Log(selected_game_object.transform.name);
                selected_game_object.GetComponent<Interactable>().Interact(this, null);
            }
        }
    }
}
