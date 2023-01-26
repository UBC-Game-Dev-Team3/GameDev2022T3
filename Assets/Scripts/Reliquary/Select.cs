using StarterAssets;
using TMPro;
using UnityEngine;
using Util;

namespace Reliquary
{
    public class Select : MonoBehaviour
    {
        public Camera cm;
        public TextMeshProUGUI itemName;

        public float viewRange = 15f;

        private GameObject _selectedGameObject;

        private StarterAssetsInputs _input;

        private void Awake()
        {
            _input = FindObjectOfType<StarterAssetsInputs>();
        }

        private void Update()
        {
            if (Physics.Raycast(cm.transform.position, cm.transform.forward, out RaycastHit hitInfo, viewRange))
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

            if (!_input.select) return;
            if (_selectedGameObject)
            {
                Debug.Log(_selectedGameObject.transform.name);
                _selectedGameObject.GetComponent<Interactable>().Interact(this, null);
            }

            _input.select = false;
        }
    }
}
