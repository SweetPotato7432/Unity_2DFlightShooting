using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 위치 값을 CSV 파일에서 불러옴
    // No, x, y 
    [SerializeField]
    private List<List<string>> csvData = new List<List<string>>();
    [SerializeField]
    private List<Vector3> spawnPoints = new List<Vector3>();

    [SerializeField]
    List<int> randomIndex = new List<int>();
    int lastIndex = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        //randomIndex = new LinkedList<int>(temp) ;

        // TextAsset : 
        // csv파일을 불러오고 있을시
        TextAsset csvFile = Resources.Load<TextAsset>("EnemySpawnPos");
        if (csvFile != null)
        {
            Debug.Log("파일이 존재합니다.");
            // '\n' : 줄바꿈
            // 줄바꿈을 찾아서 분리한다.
            string[] rows = csvFile.text.Split('\n');

            foreach (string row in rows)
            {
                string[] fields = row.Split(',');
                List<string> rowData = new List<string>(fields);
                csvData.Add(rowData);
            }
            int row_num = 0;
            foreach (List<string> row in csvData)
            {
                randomIndex.Add(row_num);
                spawnPoints.Add(new Vector3(int.Parse(row[1]), int.Parse(row[2]), 0));

                //Debug.Log("[" + (row_num + 1) + "]행");
                //Debug.Log($"1열 : {int.Parse(row[0])}");
                //Debug.Log($"2열 : {int.Parse(row[1])}");
                //Debug.Log($"3열 : {float.Parse(row[2])}");
                row_num++;
            }

        }
        else
        {
            Debug.LogError("파일이 존재하지 않습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        }
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
}
