using UnityEngine;
using UnityEngine.Events;

namespace Util
{
    /// <summary>
    ///     Script attached to an object that can be interacted with
    /// </summary>
    public class Interactable : MonoBehaviour
    {
        [Tooltip("Event(s) that run upon interaction")]
        public UnityEvent eventOnInteract = new UnityEvent();

        // this is probably unneeded since it's 3D rather than 2D
        // but i swear if i hear a 'hey this is a code smell you should extract cl-'
        // My response: https://img.ifunny.co/videos/8aebe131a0f5c1e0f55d773fae71f8ca0aafc931c142910859aeada440020156_1.mp4
        //[Tooltip("Priority of interaction (used in sorting order for player interaction)")]
        //public int priority;

        /// <summary>
        ///     Interact with this object by triggering the events on interact
        /// </summary>
        /// <param name="src">Object that interacted with this object</param>
        /// <param name="args">Optional arguments</param>
        public virtual void Interact(object src, params object[] args)
        {
            eventOnInteract.Invoke();
        }
    }
}