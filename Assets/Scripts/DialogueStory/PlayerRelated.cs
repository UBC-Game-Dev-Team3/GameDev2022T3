using StarterAssets;
using UnityEngine;

namespace DialogueStory
{
    /// <summary>
    /// Hey uh I haven't yet *disabled* it so this is needed since I cba to do that part now
    /// </summary>
    public class PlayerRelated
    {
        private static FirstPersonController _player;

        private static FirstPersonController player
        {
            get
            {
                if (!_player)
                {
                    _player = Object.FindObjectOfType<FirstPersonController>();
                }

                if (!_player)
                {
                    Debug.LogWarning("._.");
                }

                return _player;
            }
        }

        /// <summary>
        /// If true, the player can move
        /// </summary>
        public static bool MovementEnabled
        {
            get => player.movementEnabled;
            set => player.movementEnabled = value;
        }

        /// <summary>
        /// If true, the player can interact
        ///
        /// taken from the old code: would probably switch with whatever interaction system we're using
        /// </summary>
        public static bool InteractionEnabled
        {
            get => _interactionEnabled;
            set => _interactionEnabled = value;
        }
        
        private static bool _interactionEnabled = true;

        public static bool ShouldListenForUIOpenEvents
        {
            get => player.shouldListenToUIEvents;
            set => player.shouldListenToUIEvents = value;
        }
    }
}