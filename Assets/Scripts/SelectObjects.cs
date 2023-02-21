using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;

public class SelectObjects : MonoBehaviour
{
    public Camera cm;
    public TextMeshProUGUI item_name;
   
    public float view_range = 15f;
    private SelectableObject _currentObj;
    private InputActionMap _actions;

    private void Awake()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        _actions = input.actions.FindActionMap("Player");
        _actions.FindAction("Select").performed += ctx => Select();
    }

    private void OnEnable()
    {
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }

    private void Select()
    {
        if (_currentObj)
        {
            _currentObj.Interact(this);
        }
    }

    private RaycastHit _hitInfo;

    private void FixedUpdate()
    {
        SelectableObject previous = _currentObj;
        if (!Physics.Raycast(cm.transform.position, cm.transform.forward, out _hitInfo, view_range))
        {
            if (previous == null) return;
            item_name.text = "";
            previous.Deselect();

            return;
        }
        _currentObj = _hitInfo.collider.gameObject.GetComponent<SelectableObject>();
        if (_currentObj == null)
        {
            if (previous == null) return;
            item_name.text = "";
            previous.Deselect();
        } else if (_currentObj != previous)
        {
            if (previous != null) previous.Deselect();

            item_name.text = _currentObj.ObjectName; //display item name
            _currentObj.Select();
        }
    }
}
