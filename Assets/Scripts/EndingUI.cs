using Reliquary;
using UnityEngine;
using Util;

public class EndingUI : MonoBehaviour
{
    public ReliquaryPuzzle[] puzzles;
    private int numberSuccessful = 0;
    
    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        for (int i = 0; i < puzzles.Length; i++)
        {
            puzzles[i].onFailure.AddListener(OnFailure);
            puzzles[i].onSuccess.AddListener(OnSuccess);
        }
    }

    private void OnSuccess()
    {
        UpdateCount();
        if (numberSuccessful != puzzles.Length) return;
        PlayerRelated.TriggerUIOpen();
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnFailure()
    {
        UpdateCount();
    }

    private void UpdateCount()
    {
        numberSuccessful = 0;
        foreach (ReliquaryPuzzle t in puzzles)
        {
            if (t.solved) numberSuccessful++;
        }
        Debug.Log(numberSuccessful);
    }
}