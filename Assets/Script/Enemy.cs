using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public override int maxHP { get; set; }
    public override int curHP { get; set; }
    public override float moveSpeed { get; set; }
    public override int atk { get; set; }
    public override float atkSpeed { get; set; }

    [SerializeField]
    private Player player;

    bool isFirstDeactive = true;

    GameManager gameManger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        gameManger = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Initalize(List<string> enemyStat, Vector3 position)
    {
        gameObject.SetActive(true);
        int field_num = 0;
        foreach (string field in enemyStat)
        {
            switch (field_num)
            {
                case 0:
                    break;
                case 1:
                    maxHP = int.Parse(field);
                    break;
                case 2:
                    moveSpeed = float.Parse(field);
                    break;
                case 3:
                    atk = int.Parse(field);
                    break;
                case 4:
                    atkSpeed = float.Parse(field);
                    break;
            }
            field_num++;
        }
        curHP = maxHP;
        transform.position = position;
    }


    public override void Attack()
    {
        
    }

    public override void Move()
    {
        // 플레이어 캐릭터에게 이동
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
        }
    }

    // 비활성화
    public override void Deactive()
    {
        if (isFirstDeactive)
        {
            isFirstDeactive = false;
        }
        else
        {
            gameManger.AddScore(100);
        }
        // 캐릭터 사망
        gameObject.SetActive(false);
        EnemyPoolManager.Instance.ReturnEnemy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("충돌"+collision.gameObject.name);
        // 충돌한 오브젝트에게 데미지
        
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.TakeDamage(atk);
            Deactive();
        }
    }
}
