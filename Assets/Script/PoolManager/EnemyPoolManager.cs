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

    protected List<List<string>> csvData = new List<List<string>>();

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

        //InitializePool();
    }

    private void Start()
    {
        CSVLoading csvloading = new CSVLoading();
        csvData = csvloading.csvLoad("CharacterStat");
    }
    // Ǯ �ʱ�ȭ
    public void InitializePool()
    {
        enemyPool = new Queue<Enemy>();
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
        //enemyPool.Enqueue(enemy);
    }

    // �� ���
    public Enemy GetEnemy(Vector3 position)
    {
        if(enemyPool.Count == 0)
        {
            CreateNewEnemy();
        }

        // �� Ÿ�� ������ ��������
        int characterNum = 1;

        Enemy enemy = enemyPool.Dequeue();
        enemy.Initalize(csvData[characterNum] ,position);

        return enemy;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        enemyPool.Enqueue(enemy);
    }
}
