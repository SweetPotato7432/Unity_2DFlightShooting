using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 추후에 위치 값을 CSV 파일에서 불러옴
    [SerializeField]
    private Transform[] spawnPoints; // 등장 위치 --> csv 교체

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 적 제거시
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
