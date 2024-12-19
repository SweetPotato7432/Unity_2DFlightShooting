using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    // �̱��� : Ŭ������ �ν��Ͻ� ȭ ��Ű�� �ʾƵ� ��� ����
    // ���� ������ �Ѱ��� ��ü�� �����ؾ߸� �Ѵ�.
    public static EnemyPoolManager Instance { get; private set; }

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int initialPoolSize = 10; // �ʱ� Ǯ ũ��

    private Queue<Enemy> enemyPool= new Queue<Enemy>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // ������ �÷��Ϳ� �޸� �ݳ�
            return;
        }

        DontDestroyOnLoad(gameObject); // �ٸ� �������� ���

        InitializePool();
    }

    // Ǯ �ʱ�ȭ
    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewEnemy();
        }

    }

    // �� ����
    private void CreateNewEnemy()
    {
        GameObject enemyObject = Instantiate(enemyPrefab);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.Deactive();
        enemyPool.Enqueue(enemy);
    }

    // �� ���
    public Enemy GetEnemy(Vector3 position)
    {
        if(enemyPool.Count == 0)
        {
            CreateNewEnemy();
        }

        Enemy enemy = enemyPool.Dequeue();
        enemy.Initalize(position);

        return enemy;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        enemy.Deactive();
        enemyPool.Enqueue(enemy);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
