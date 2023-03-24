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
        private void Awake()
        {
            _reliquaryTrigger = GetComponent<ReliquaryTriggerObject>();
            _reliquaryTrigger.puzzle.onSuccess.AddListener(OnSuccess);
            _reliquaryTrigger.puzzle.onFailure.AddListener(OnFailure);
            _source = GetComponent<AudioSource>();
        }

        public void OnFailure()
        {
            
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