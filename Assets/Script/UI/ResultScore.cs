using UnityEngine;
using TMPro;

public class ResultScore : MonoBehaviour
{
    float duration = 1.5f;

    GameManager gameManger;

    int targetScore = 0;

    float deltaTime = 0f;

    TMP_Text resultText;

    bool isRun = false;

    float lerp = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void Update()
    {
        if (isRun)
        {
            int progressScore = ProgressScore();
            resultText.text = $"SCORE : {progressScore}";

            if (progressScore == targetScore)
            {
                isRun = false;
            }
        }
        
    }

    void OnEnable()
    {
        gameManger = FindFirstObjectByType<GameManager>();
        resultText = GetComponent<TMP_Text>();

        targetScore = gameManger.GetScore();
        deltaTime = 0f;
        lerp = 0f;
        isRun = true;
    }

    int ProgressScore()
    {
        int progressScore;

        deltaTime += Time.deltaTime;
        lerp += Time.deltaTime / duration;
        progressScore = (int)Mathf.Lerp(0, targetScore, lerp);
        return progressScore;
    }

}

