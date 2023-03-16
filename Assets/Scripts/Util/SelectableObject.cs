using UnityEngine;

namespace Util
{
    /// <summary>
    /// Attach to an object to be highlighted with a tooltip text
    /// </summary>
    [RequireComponent(typeof(Outline))]
    public class SelectableObject : Interactable
    {
        private Outline _outline;

        public virtual string TooltipText => gameObject.name;
        public virtual bool HasTooltip => true;

        private void Awake()
        {
            _outline = GetComponent<Outline>();
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