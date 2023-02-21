using UnityEngine;

namespace Util
{
    public static class Utilities
    {
        public static void InstantiateToLength(GameObject prefab, Transform parent, int desired)
        {
            int children = parent.childCount;
            for (int i = children - 1; i >= desired; i--)
            {
                Object.Destroy(parent.transform.GetChild(i).gameObject);
            }
            for (int i = children; i < desired; i++)
            {
                Object.Instantiate(prefab, parent);
            }
        }
    }
}