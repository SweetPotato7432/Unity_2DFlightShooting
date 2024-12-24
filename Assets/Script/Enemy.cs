using UnityEngine;

public class Enemy : Character
{
    public override int hp { get; set; } = 5;
    public override float moveSpeed { get; set; } = 2f;
    public override int atk { get; set; } = 10;
    public override float atkSpeed { get; set; } = 0;

    [SerializeField]
    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>().GetComponent<Player>();
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

    public void Initalize(Vector3 position)
    {
        gameObject.SetActive(true);
        hp = 5;
        moveSpeed = 2f;
        atk = 10;
        atkSpeed = 0;
        transform.position = position;
    }


    public override void Attack()
    {
        
    }

    public override void Move()
    {
        // �÷��̾� ĳ���Ϳ��� �̵�

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
    }

    // ��Ȱ��ȭ
    public override void Deactive()
    {
        // ĳ���� ���
        gameObject.SetActive(false);
        EnemyPoolManager.Instance.ReturnEnemy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("�浹"+collision.gameObject.name);
        // �浹�� ������Ʈ���� ������
        
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.TakeDamage(atk);
            Deactive();
        }
    }
}
