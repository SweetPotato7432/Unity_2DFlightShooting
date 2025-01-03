using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 위치 값을 CSV 파일에서 불러옴
    // No, x, y 
    
    private List<List<string>> csvData = new List<List<string>>();
    private List<Vector3> spawnPoints = new List<Vector3>();

    private float spawnCooltime = 1f;
    private float time = 0;

    List<int> randomIndex = new List<int>();
    int lastIndex = -1;

    [SerializeField]
    int score = 0;
    [SerializeField]
    private Player player;
    [SerializeField]
    TMP_Text scoreText;

    [SerializeField]
    private GameObject resultMenu;

    bool isTutorial = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyPoolManager.Instance.InitializePool();

        BulletPoolManager.Instance.InitializePool();
        EffectPoolManager.Instance.InitializePool();

        player = FindFirstObjectByType<Player>();

        CSVLoading csvLoading = new CSVLoading();

        csvData = csvLoading.csvLoad("EnemySpawnPos");

        int row_num = 0;
        foreach (List<string> row in csvData)
        {
            randomIndex.Add(row_num);
            spawnPoints.Add(new Vector3(int.Parse(row[1]), int.Parse(row[2]), 0));
            row_num++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isTutorial)
        {
            // 본게임 코드
            time += Time.deltaTime;
            if (time >= spawnCooltime)
            {
                player = FindFirstObjectByType<Player>();
                if (player != null)
                {
                    SpawnEnemy();
                    time -= spawnCooltime;
                }
            }

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    SpawnEnemy();
            //}
        }
        else
        {
            // 튜토리얼 코드

        }


        scoreText.text = score.ToString();

    }

    public void SpawnEnemy()
    {
        //int spawnIndex = Random.Range(0, spawnPoints.Count);
        int Index = Random.Range(0,randomIndex.Count);
        int spawnIndex = randomIndex[Index];
        Vector3 spawnPosition = spawnPoints[spawnIndex];

        randomIndex.Remove(spawnIndex);
        if (lastIndex != -1)
        {
            randomIndex.Add(lastIndex);
        }
        lastIndex = spawnIndex;

        Enemy enemy = EnemyPoolManager.Instance.GetEnemy(spawnPosition);
        //Debug.Log($"Spawned Enemy at {spawnPosition}");

    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        GameSettingData.Instance.SaveScore(score);
        resultMenu.gameObject.SetActive(true);
    }

    public void TutorialStart()
    {
        isTutorial = true;
    }
}
