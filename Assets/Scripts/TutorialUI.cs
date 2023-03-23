using UnityEngine;
using Util;

public class TutorialUI : MonoBehaviour
{
    private void Awake()
    {
        PlayerRelated.TriggerUIOpen();
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnButtonPress()
    {
        PlayerRelated.TriggerUIClose();
        Destroy(gameObject);
    }
}