using System;
using System.Linq;
using SymbolBook;
using UnityEngine;
using UnityEngine.Events;

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

        public UnityEvent onSuccess;
        public UnityEvent onFailure;
        
        /// <summary>
        /// 
        /// </summary>
        [Serializable]
        public struct Ring
        {
            [Tooltip("All the possible options for the ring")]
            public Symbol[] options;
            [NonSerialized]
            public int SelectedIndex;
            [Tooltip("Correct Answer")]
            public Symbol correctAnswer;

            public void SetSolved()
            {
                for (int j = 0; j < options.Length; j++)
                {
                    if (correctAnswer.symbolName != options[j].symbolName) continue;
                    SelectedIndex = j;
                    return;
                }
                Debug.LogWarning("Ring cannot be solved!");
            }
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

        public bool solved
        {
            get
            {
                return rings.All(r => r.options[r.SelectedIndex].symbolName == r.correctAnswer.symbolName);
            }
        }
    }
}