using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    // 싱글톤 : 클래스를 인스턴스 화 시키지 않아도 사용 가능
    // 게임 내에서 한개의 객체만 존재해야만 한다.
    public static EnemyPoolManager Instance { get; private set; }

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int initialPoolSize = 10; // 초기 풀 크기

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
            Destroy(gameObject); // 가바지 컬렉터에 메모리 반납
            return;
        }

        DontDestroyOnLoad(gameObject); // 다른 씬에서도 사용

        //InitializePool();
    }

    private void Start()
    {
        CSVLoading csvloading = new CSVLoading();
        csvData = csvloading.csvLoad("CharacterStat");
    }
    // 풀 초기화
    public void InitializePool()
    {
        enemyPool = new Queue<Enemy>();
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewEnemy();
        }

    }

    // 적 생성
    private void CreateNewEnemy()
    {
        GameObject enemyObject = Instantiate(enemyPrefab);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.Deactive();
        //enemyPool.Enqueue(enemy);
    }

    // 적 사용
    public Enemy GetEnemy(Vector3 position)
    {
        if(enemyPool.Count == 0)
        {
            CreateNewEnemy();
        }

        // 적 타입 증가시 랜덤으로
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
