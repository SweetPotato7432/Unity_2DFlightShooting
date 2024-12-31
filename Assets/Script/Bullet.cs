using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // bullet Type.csv로 관리

    private float bulletVelocity =19f;
    private int atk;
    private Vector3 bulletDir;
    private string targetTag;

    bool isFirstDeactive = true;

    private SpriteRenderer spriteRenderer;
    
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
        if(tag == "Player")
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
        else
        {
            EffectPoolManager.Instance.GetEffect(transform.position, Effect.AnimType.BulletHit);
        }
        StopAllCoroutines();
        //Debug.Log("제거");
        gameObject.SetActive(false);
        BulletPoolManager.Instance.ReturnBullet(this);
    }

    IEnumerator ActiveTimeLimit()
    {
        yield return new WaitForSeconds(3.0f);
        Deactive();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("충돌"+collision.gameObject.name);
        // 충돌한 오브젝트에게 데미지
        if(collision.tag == targetTag)
        {
            Character character = collision.GetComponent<Character>();
            character.TakeDamage(atk);
            Deactive();
        }
        
    }

}
