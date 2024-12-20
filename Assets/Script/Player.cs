using System.Collections;
using UnityEngine;

public class Player : Character
{
    public FixedJoystick fixedJoystick;

    public override int hp { get; set; } = 50;
    public override float moveSpeed { get; set; } = 3f;
    public override int atk { get; set; } = 5;
    public override float atkSpeed { get; set; } = 0.5f;

    [SerializeField]
    private bool canAttack;

    [SerializeField]
    Transform shootPosition;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canAttack = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Debug.Log($"현재 체력 {hp}");
    }

    public void FixedUpdate()
    {
        Move();
    }
    public override void Attack()
    {
        if( canAttack)
        {
            
            Debug.Log("생성");
            Bullet bullet = BulletPoolManager.Instance.GetBullet(this.tag, shootPosition.position, atk);
            StartCoroutine(AttackCoolTime());
        }
        
    }

    IEnumerator AttackCoolTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(1/atkSpeed);
        canAttack = true;

    }

    public override void Deactive()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        Vector3 direction = Vector3.up * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        direction = direction.normalized * moveSpeed * Time.deltaTime;
        transform.position += direction;
    }
}
