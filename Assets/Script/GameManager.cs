using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ��ġ ���� CSV ���Ͽ��� �ҷ���
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
        // csv������ �ҷ����� ������
        TextAsset csvFile = Resources.Load<TextAsset>("EnemySpawnPos");
        if (csvFile != null)
        {
            Debug.Log("������ �����մϴ�.");
            // '\n' : �ٹٲ�
            // �ٹٲ��� ã�Ƽ� �и��Ѵ�.
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

                //Debug.Log("[" + (row_num + 1) + "]��");
                //Debug.Log($"1�� : {int.Parse(row[0])}");
                //Debug.Log($"2�� : {int.Parse(row[1])}");
                //Debug.Log($"3�� : {float.Parse(row[2])}");
                row_num++;
            }

        }
        else
        {
            Debug.LogError("������ �������� �ʽ��ϴ�.");
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
