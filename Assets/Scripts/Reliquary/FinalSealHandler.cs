using System;
using System.Collections;
using UnityEngine;

namespace Reliquary
{
    [RequireComponent(typeof(ReliquaryTriggerObject), typeof(AudioSource))]
    public class FinalSealHandler : MonoBehaviour
    {
        public Vector3 positionOffset = new Vector3(0, 0, -1);
        [Min(0)]
        public float timeTaken = 1;

        private AudioSource _source;
        private ReliquaryTriggerObject _reliquaryTrigger;
        private ReliquaryPuzzle _puzzle;
        private void Awake()
        {
            _reliquaryTrigger = GetComponent<ReliquaryTriggerObject>();
            _puzzle = _reliquaryTrigger.puzzle;
            _puzzle.onSuccess.AddListener(OnSuccess);
            _puzzle.onFailure.AddListener(OnFailure);
            _source = GetComponent<AudioSource>();
        }

        public void OnFailure()
        {
            
        }

        public void TriggerPuzzleSuccess()
        {
            for (int i = 0; i < _puzzle.rings.Length; i++)
            {
                _puzzle.rings[i].SetSolved();
            }
            if (!_puzzle.solved)
            {
                Debug.Log("BRUH");
            }
            _puzzle.onSuccess.Invoke();
        }

        public void OnSuccess()
        {
            StartCoroutine(MoveCoroutine());
            _reliquaryTrigger.enabled = false;
            _source.Play();
        }

        private IEnumerator MoveCoroutine()
        {
            Vector3 initialPosition = transform.localPosition;
            for (float i = 0; i < timeTaken; i += Time.deltaTime)
            {
                transform.localPosition += positionOffset * Time.deltaTime / timeTaken;
                yield return null;
            }

            transform.localPosition = initialPosition + positionOffset;
        }
    }
}