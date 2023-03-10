using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Reliquary
{
    [RequireComponent(typeof(Button),typeof(RectTransform))]
    public class ReliquaryRing : MonoBehaviour
    {
        public ReliquaryUI ui { get; set; }
        public GameObject rotSpritePrefab;
        public int centerSize;
        public int ringSize;
        [Tooltip("Time it takes to make one rotation"), Range(0.01f,10)]
        public float rotationTime = 1.5f;

        private ReliquaryPuzzle _puzzle;
        private RectTransform _rectTransform;
        private Button _button;
        private int _index;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _button = GetComponent<Button>();
            GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        }

        public void UpdateRing(ReliquaryPuzzle puzzle, int index)
        {
            _index = index;
            _puzzle = puzzle;
            ReliquaryPuzzle.Ring ring = puzzle.rings[index];
            float initialRotation = puzzle.rings[index].SelectedIndex * 360f / _puzzle.rings[index].options.Length;
            transform.localEulerAngles = new Vector3(0, 0, initialRotation);
            int size = centerSize + (ringSize * (puzzle.rings.Length - index));
            _rectTransform.sizeDelta = new Vector2(size,size);
            Utilities.InstantiateToLength(rotSpritePrefab, transform, ring.options.Length);
            for (int j = 0; j < ring.options.Length; j++)
            {
                Transform rotObj = transform.GetChild(j);
                float angle = j * 360f / ring.options.Length;
                rotObj.localEulerAngles = new Vector3(0, 0, angle);
                    
                Image image = rotObj.GetComponentInChildren<Image>();
                ring.options[j].Render(image.gameObject);
            }
        }

        public void OnButtonClick()
        {
            ui.OnRingClick(_index);
            StartCoroutine(RotateCoroutine());
        }

        public void OnDisable()
        {
            StopCoroutine(nameof(RotateCoroutine));
            _button.interactable = true;
        }

        private IEnumerator RotateCoroutine()
        {
            float rotationAmount = -360f/_puzzle.rings[_index].options.Length;
            _button.interactable = false;
            float currTime = 0;
            while (currTime < rotationTime)
            {
                float perFrameRotationAmount = Time.deltaTime * rotationAmount / rotationTime;
                currTime += Time.deltaTime;
                transform.localEulerAngles += new Vector3(0, 0, perFrameRotationAmount);
                
                yield return null;
            }

            _button.interactable = true;
        }
    }
}