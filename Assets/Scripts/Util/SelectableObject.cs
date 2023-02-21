using System;
using UnityEngine;

namespace Util
{
    /// <summary>
    /// Attach to an object to be highlighted
    /// </summary>
    [RequireComponent(typeof(Outline))]
    public class SelectableObject : Interactable
    {
        private Outline _outline;
        [Tooltip("Object Name")]
        public string ObjectName;
        private void Awake()
        {
            _outline = GetComponent<Outline>();
        }

        public override void Interact(object src, params object[] args)
        {
            base.Interact(src, args);
            Debug.Log("dkajfakldjfa");
        }

        public void Select()
        {
            _outline.enabled = true;
        }

        public void Deselect()
        {
            _outline.enabled = false;
        }
    }
}