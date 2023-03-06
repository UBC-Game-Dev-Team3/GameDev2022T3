using UnityEngine;
using UnityEngine.Events;
using Util;

namespace DialogueStory
{
    [RequireComponent(typeof(Collider2D))]
    public class DialoguePlayerExitScript : MonoBehaviour
    {
        [Tooltip("Unity event called on exit")]
        public UnityEvent eventOnExit = new UnityEvent();
        public void OnTriggerExit2D(Collider2D other)
        {
            DialogueManager.Instance.Close();
            PlayerRelated.InteractionEnabled = true;
            eventOnExit.Invoke();
        }
    }
}