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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        }

        scoreText.text = score.ToString();
        
    }

    private void SpawnEnemy()
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
        Debug.Log($"Spawned Enemy at {spawnPosition}");
    }

    public void AddScore(int score)
    {
        this.score += score;
    }
}
