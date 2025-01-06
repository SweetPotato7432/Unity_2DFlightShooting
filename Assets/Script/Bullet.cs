using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // bullet Type.csv�� ����

    private float bulletVelocity =19f;
    private int atk;
    private Vector3 bulletDir;
    private string targetTag;

    bool isFirstDeactive = true;

    private SpriteRenderer spriteRenderer;

    bool isTimeEnd = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition = Vector3.MoveTowards(transform.position, transform.position + bulletDir, Time.deltaTime * bulletVelocity);
    }

    public void Initalize(string tag, Vector3 position, int atk)
    {
        isTimeEnd = false;
        if (tag == "Player")
        {
            //spriteRenderer.flipY = false;
            bulletVelocity = 10f;
            this.atk = atk;
            bulletDir = Vector3.up;
            targetTag = "Enemy";
        }
        else if (tag == "Enemy")
        {
            //spriteRenderer.flipY = true;
            bulletVelocity = 6f;
            this.atk = atk;
            bulletDir = Vector3.down;
            targetTag = "Player";
        }
        gameObject.SetActive(true);

        transform.position = position;
        StartCoroutine(ActiveTimeLimit());
    }

    public void Deactive()
    {
        if (isFirstDeactive)
        {
            isFirstDeactive = false;
        }
        else if(!isTimeEnd)
        {
            EffectPoolManager.Instance.GetEffect(transform.position, Effect.AnimType.BulletHit);
        }
        StopAllCoroutines();
        //Debug.Log("����");
        gameObject.SetActive(false);
        BulletPoolManager.Instance.ReturnBullet(this);
    }

    IEnumerator ActiveTimeLimit()
    {
        isTimeEnd = false;
        yield return new WaitForSeconds(3.0f);
        isTimeEnd = true;
        Deactive();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("�浹"+collision.gameObject.name);
        // �浹�� ������Ʈ���� ������
        if(collision.tag == targetTag)
        {
            Character character = collision.GetComponent<Character>();
            character.TakeDamage(atk);
            Deactive();
        }
        
    }

}
