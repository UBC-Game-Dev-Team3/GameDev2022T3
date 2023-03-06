using TMPro;
using TranslationUI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Util;

public class SelectObjects : MonoBehaviour
{
    public Camera cm;
    [FormerlySerializedAs("item_name")] public TextMeshProUGUI itemNameUI;
   
    [FormerlySerializedAs("view_range")] public float viewRange = 15f;
    private SelectableObject _currentObj;
    private InputActionMap _actions;
    private bool _enabled = true;

    private void Awake()
    {
        _actions = FindObjectOfType<PlayerInput>().actions.FindActionMap("Player");
        _actions.FindAction("Select").performed += Select;
    }

    private void OnDestroy()
    {
        _actions.FindAction("Select").performed -= Select;
    }

    private void OnEnable()
    {
        _enabled = true;
    }

    private void OnDisable()
    {
        _enabled = false;
    }

    private void Select(InputAction.CallbackContext ctx)
    {
        if (!_currentObj || !_enabled || !PlayerRelated.InteractionEnabled) return;
        _currentObj.Interact(this);
    }

    private RaycastHit _hitInfo;

    private void FixedUpdate()
    {
        SelectableObject previous = _currentObj;
        if (!Physics.Raycast(cm.transform.position, cm.transform.forward, out _hitInfo, viewRange))
        {
            if (previous == null) return;
            itemNameUI.text = "";
            previous.Deselect();

            return;
        }
        _currentObj = _hitInfo.collider.gameObject.GetComponent<SelectableObject>();
        if (_currentObj == null)
        {
            if (previous == null) return;
            itemNameUI.text = "";
            previous.Deselect();
        } else if (_currentObj != previous)
        {
            if (previous != null) previous.Deselect();

            itemNameUI.text = _currentObj.TooltipText;
            _currentObj.Select();
        }
    }
}
