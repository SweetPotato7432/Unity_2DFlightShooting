using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public VariableJoystick varialbeJoystick;
    public bool btnDown;

    public override int hp { get; set; } = 50;
    public override float moveSpeed { get; set; } = 3f;
    public override int atk { get; set; } = 5;
    public override float atkSpeed { get; set; } = 0.2f;

    [SerializeField]
    private bool canAttack;

    [SerializeField]
    Transform shootPosition;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canAttack = true;
        btnDown = false;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //float speed = rb.linearVelocity.magnitude;
        //Debug.Log("현재 속도(2D): " + speed.ToString());

        //Debug.Log(hp);

        if (btnDown)
        {
            Attack();
        }


    }

    public void FixedUpdate()
    {
        Move();
    }
    public override void Attack()
    {
        if(canAttack)
        {
            Bullet bullet = BulletPoolManager.Instance.GetBullet(this.tag, shootPosition.position, atk);
            StartCoroutine(AttackCoolTime());
        }
        
    }

    IEnumerator AttackCoolTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(atkSpeed);
        canAttack = true;

    }

    public void BtnDown()
    {
        btnDown = true;
    }
    public void BtnUp()
    {
        btnDown = false;
    }

    public override void Deactive()
    {
        gameObject.SetActive(false);
    }

    public override void Move()
    {
        //Vector3 direction = Vector3.up * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        //direction = direction * moveSpeed * Time.deltaTime;
        //transform.position += direction;
        Vector3 direction = Vector3.up * varialbeJoystick.Vertical + Vector3.right * varialbeJoystick.Horizontal;
        float magnitude = direction.magnitude;
        magnitude = Mathf.Clamp01(magnitude);

        direction = direction * magnitude * moveSpeed*50 * Time.deltaTime;
        
        //transform.position += direction;
        rb.linearVelocity = direction;
    }


}
