using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ���Ŀ� ��ġ ���� CSV ���Ͽ��� �ҷ���
    [SerializeField]
    private Transform[] spawnPoints; // ���� ��ġ --> csv ��ü

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �� ���Ž�
        // EnemyPoolManager.Instance.ReturnEnemy();
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
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[spawnIndex].position;

        Enemy enemy = EnemyPoolManager.Instance.GetEnemy(spawnPosition);
        Debug.Log($"Spawned Enemy at {spawnPosition}");
    }
}
