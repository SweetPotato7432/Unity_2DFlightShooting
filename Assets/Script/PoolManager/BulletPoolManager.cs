using UnityEngine;
using System.Collections.Generic;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance { get; private set;}

    [SerializeField]
    private GameObject bulletPrefap;
    [SerializeField]
    private int initialPoolSize = 20;

    private Queue<Bullet> bulletPool = new Queue<Bullet>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        InitializePool();

        
    }

    // Ǯ �ʱ�ȭ
    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewBullet();
        }

    }

    // �� ����
    private void CreateNewBullet()
    {
        GameObject bulletObject = Instantiate(bulletPrefap);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.Deactive();
        //bulletPool.Enqueue(bullet);
    }

    // �� ���
    public Bullet GetBullet(string tag, Vector3 position, int atk)
    {
        if (bulletPool.Count == 0)
        {
            CreateNewBullet();
        }

        Bullet bullet = bulletPool.Dequeue();
        bullet.Initalize(tag, position, atk);

        return bullet;
    }

    public void ReturnBullet(Bullet bullet)
    {
        bulletPool.Enqueue(bullet);
    }
}
