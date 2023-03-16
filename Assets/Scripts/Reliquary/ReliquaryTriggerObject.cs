using UnityEngine;
using Util;

namespace Reliquary
{
    public class ReliquaryTriggerObject : SelectableObject
    {
        [Tooltip("Puzzle")]
        public ReliquaryPuzzle puzzle;
        [Tooltip("Reliquary UI to enable")]
        public ReliquaryUI reliquaryUI;

        public override bool HasTooltip => false;

        public override void Interact(object src, params object[] args)
        {
            base.Interact(src, args);
            reliquaryUI.OpenUI(puzzle);
        }
    }
}