using TMPro;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    private int[] rankScore = new int[10];

    [SerializeField]
    GameObject[] rankTextGameObject = new GameObject[10];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int currentPlayerRank = PlayerPrefs.GetInt("CurrentPlayerRank");
        for (int i = 0; i < 10; i++)
        {
            TMP_Text rankText = rankTextGameObject[i].transform.GetChild(0).GetComponent<TMP_Text>();
            TMP_Text scoreText = rankTextGameObject[i].transform.GetChild(1).GetComponent<TMP_Text>();

            rankScore[i] = PlayerPrefs.GetInt(i + "BestScore");
            scoreText.text = rankScore[i].ToString();

            if(i == currentPlayerRank)
            {
                Color Rank = new Color(255, 0, 0);
                rankText.color = Rank;
                scoreText.color = Rank;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
