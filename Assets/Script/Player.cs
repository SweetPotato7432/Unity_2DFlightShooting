using System.Collections;
using Unity.VisualScripting;
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

    bool isTopTouched;
    bool isBottomTouched;
    bool isLeftTouched;
    bool isRightTouched;

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
        float h = varialbeJoystick.Horizontal;
        float v = varialbeJoystick.Vertical;

        if ((isRightTouched && h > 0) || (isLeftTouched && h < 0))
        {
            h = 0;
        }
        if ((isTopTouched && v > 0) || (isBottomTouched && v < 0))
        {
            v = 0;
        }

        Vector3 direction = Vector3.up * v + Vector3.right * h;
        float magnitude = direction.magnitude;
        magnitude = Mathf.Clamp01(magnitude);

        direction = direction * magnitude * moveSpeed*50 * Time.deltaTime;
        
        //transform.position += direction;
        rb.linearVelocity = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTopTouched = true;
                    break;
                case "Bottom":
                    isBottomTouched = true;
                    break;
                case "Left":
                    isLeftTouched = true;
                    break;
                case "Right":
                    isRightTouched = true;
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTopTouched = false;
                    break;
                case "Bottom":
                    isBottomTouched = false;
                    break;
                case "Left":
                    isLeftTouched = false;
                    break;
                case "Right":
                    isRightTouched = false;
                    break;
            }
        }
    }
}
