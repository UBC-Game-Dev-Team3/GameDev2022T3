using UnityEngine;

namespace Inventory
{
    /// <summary>
    ///     Represents an item.
    /// </summary>
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public new string name = "New Item";
        public Sprite icon;
        public GameObject prefab;
        [TextArea]
        public string description = "";
        [Tooltip("Whether the item is usable")]
        public bool isUsable = false;

        /// <summary>
        /// Use the current item
        /// </summary>
        public virtual void Use()
        {
        }

        public override bool Equals(object other)
        {
            if (!(other is Item item)) return false;
            if (item == null) return false;
            return name.Equals(item.name) && icon.Equals(item.icon) && description.Equals(item.description);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = 31 * hash + (name == null ? 0 : name.GetHashCode());
            hash = 31 * hash + (icon == null ? 0 : icon.GetHashCode());
            hash = 31 * hash + (description == null ? 0 : description.GetHashCode());
            return hash;
        }
    }
}