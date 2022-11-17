using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class InventoryImage : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerUpHandler, IBeginDragHandler
    {
        public GameObject thingToRotate;

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("OnPointerClick");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnPointerExit");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (thingToRotate != null)
            {
                thingToRotate.transform.localRotation *= Quaternion.Euler(thingToRotate.transform.up * -eventData.delta.x * 0.1f);
                thingToRotate.transform.localRotation *= Quaternion.Euler(thingToRotate.transform.right * eventData.delta.y * 0.1f);
            }
            Debug.Log("OnDrag: " + eventData.delta);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag: " + eventData.delta);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnPointerEnter: ");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("OnPointerUp: ");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
        }
    }
}
