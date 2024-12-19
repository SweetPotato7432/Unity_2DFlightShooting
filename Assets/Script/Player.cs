using UnityEngine;

public class Player : Character
{
    public FixedJoystick fixedJoystick;
    private Rigidbody2D rb;

    public override int hp { get; set; } = 50;
    public override float moveSpeed { get; set; } = 3f;
    public override int atk { get; set; } = 5;
    public override int atkSpeed { get; set; } = 2;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        Move();
    }
    public override void Attack()
    {
        throw new System.NotImplementedException();
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
        //rb.AddForce(direction, ForceMode2D.Impulse);
    }
}
