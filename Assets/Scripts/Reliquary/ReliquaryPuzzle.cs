using System;
using System.Linq;
using SymbolBook;
using UnityEngine;

namespace Reliquary
{
    /// <summary>
    ///     Represents a Reliquary Puzzle
    /// </summary>
    [CreateAssetMenu(fileName = "New Reliquary Puzzle", menuName = "Reliquary/Puzzle")]
    public class ReliquaryPuzzle : ScriptableObject
    {
        public Ring[] rings;
        public Symbol[] seal;
        
        /// <summary>
        /// 
        /// </summary>
        [Serializable]
        public struct Ring
        {
            [Tooltip("All the possible options for the ring")]
            public Symbol[] options;
            [NonSerialized]
            public Symbol selectedOption;
            [Tooltip("Correct Answer")]
            public Symbol correctAnswer;
        }

        public void OnValidate()
        {
            for (int i = 0; i < rings.Length; i++)
            {
                Ring ring = rings[i];
                if (ring.correctAnswer == null) continue;
                bool hasName = ring.options.Any(s => s != null && s.symbolName == ring.correctAnswer.symbolName);
                if (!hasName)
                {
                    Debug.LogWarning($"Ring index {i} correct answer {ring.correctAnswer.symbolName} not present in options!");
                }
            }
        }
    }
}