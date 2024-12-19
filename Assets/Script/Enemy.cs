using UnityEngine;

public class Enemy : Character
{
    public override int hp { get; set; } = 5;
    public override float moveSpeed { get; set; } = 2f;
    public override int atk { get; set; } = 10;
    public override int atkSpeed { get; set; } = 0;

    [SerializeField]
    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>().GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Initalize(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
    }


    public override void Attack()
    {
        // �浹�� �ڽ� �ı� �� ������ �ο�
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        // �÷��̾� ĳ���Ϳ��� �̵�
        Vector3 direction = player.transform.position-transform.position;
        direction = direction.normalized * moveSpeed * Time.deltaTime;
        transform.position += direction;
    }

    // ��Ȱ��ȭ
    public override void Deactive()
    {
        // ĳ���� ���
        gameObject.SetActive(false);
    }
}
