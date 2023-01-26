using TMPro;
using UnityEngine;
using Util;

namespace Reliquary
{
    [RequireComponent(typeof(NewStarterAssets))]
    public class Select : MonoBehaviour
    {
        public Camera cm;
        public TextMeshProUGUI itemName;

        public float viewRange = 15f;

        private GameObject _selectedGameObject;
        //private StarterAssetsInputs input;
        NewStarterAssets _inputActions;
        private void Awake()
        {
            Cursor.visible = false;
            _inputActions= new NewStarterAssets();
            _inputActions.Player.Select.performed += ctx => select_object();
        }

        private void OnEnable()
        {
            _inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Player.Disable();
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(cm.transform.position, cm.transform.forward, out hitInfo, viewRange))
            {
                if ((hitInfo.collider.gameObject.GetComponent<Interactable>()))
                {
                    //Debug.Log(hit_info.collider.name);
                    _selectedGameObject = hitInfo.collider.gameObject;
                    _selectedGameObject.GetComponent<Outline>().enabled = true;

                    itemName.text = hitInfo.collider.gameObject.name; //display item name
                }
            }
            else if (_selectedGameObject != null)
            {
                _selectedGameObject.GetComponent<Outline>().enabled = false;
                _selectedGameObject = null;
                itemName.text = string.Empty;
            }

        }

        /// <summary>
        /// On left button mouse click
        /// </summary>
        void select_object()
        {
            if (_selectedGameObject)
            {
                Debug.Log(_selectedGameObject.transform.name);
                _selectedGameObject.GetComponent<Interactable>().Interact(this, null);
            }
        }
    }
}
