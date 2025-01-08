using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    [SerializeField]
    GameManager gameManager;

    public VariableJoystick varialbeJoystick;
    public bool btnDown;

    Animator animator;

    public override int maxHP { get; set; }
    public override int curHP { get; set; }
    public override float moveSpeed { get; set; }
    public override int atk { get; set; }
    public override float atkSpeed { get; set; }

    [SerializeField]
    private bool canAttack;

    [SerializeField]
    Transform shootPosition;

    Rigidbody2D rb;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private RectTransform ui_hp_bar;

    bool isTopTouched;
    bool isBottomTouched;
    bool isLeftTouched;
    bool isRightTouched;

    [SerializeField]
    protected List<List<string>> csvData = new List<List<string>>();

    bool isInvincible;

    AudioSource shootSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootSound = GetComponent<AudioSource>();

        gameManager = FindFirstObjectByType<GameManager>();

        isInvincible = false;
        CSVLoading csvloading = new CSVLoading();
        csvData = csvloading.csvLoad("CharacterStat");

        int characterNum = 0;
        int field_num = 0;
        foreach (string field in csvData[characterNum])
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
        hpSlider.maxValue = maxHP;
        animator = GetComponent<Animator>();

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

        //Debug.Log(curHP);
        if (curHP > 0)
        {
            hpSlider.value = curHP;
        }
        else
        {
            hpSlider.gameObject.SetActive(false);
            gameManager.GameOver();

        }

        if (btnDown)
        {
            Attack();
        }



    }

    public void FixedUpdate()
    {
        Move();

        
    }

    public void LateUpdate()
    {
        //플레이어의 월드 좌표를 2D 좌표로 전송
        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);

        // 화면 해상도에 맞춰 오프셋을 조정
        float screenHeight = Screen.height; // 화면 높이
        float screenWidth = Screen.width;   // 화면 너비

        // 오프셋을 고정 비율로 계산: 화면의 높이와 너비에 비례하여 오프셋을 수정
        float offsetY = -75f;  // Y축 오프셋 (플레이어 위에 HP바를 위치시키기 위해)
        float offsetX = 0f;   // X축 오프셋 (이건 필요에 따라 설정)

        // 해상도에 맞춰 오프셋을 비례적으로 조정
        float ratioY = screenHeight / 1080f; // 기준 해상도 (1080p) 기준으로 비율을 계산
        float ratioX = screenWidth / 1920f;  // 기준 해상도 (1920p) 기준으로 비율을 계산

        // 비율에 맞춰 오프셋을 조정
        screenPos.y += offsetY * ratioY;  // 높이에 비례하여 Y 오프셋 조정
        screenPos.x += offsetX * ratioX;  // 너비에 비례하여 X 오프셋 조정 (필요에 따라)


        //screenPos.y = screenPos.y - 80f;
        ui_hp_bar.position = screenPos;

    }
    public override void Attack()
    {
        if(canAttack)
        {
            shootSound.Play();
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
        EffectPoolManager.Instance.GetEffect(transform.position, Effect.AnimType.PlayerDead);
        gameObject.SetActive(false);
    }

    public override void Move()
    {
        float h = varialbeJoystick.Horizontal;
        float v = varialbeJoystick.Vertical;

        if (h > 0.3f)
        {
            animator.SetBool("IsRight",true);
        }
        else
        {
            animator.SetBool("IsRight", false);
        }

        if (h < -0.3f)
        {
            animator.SetBool("IsLeft", true);
        }
        else
        {
            animator.SetBool("IsLeft", false);
        }

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

    public override void TakeDamage(int atk)
    {
        if (!isInvincible)
        {
            base.TakeDamage(atk);
            StartCoroutine(InvincibleCheck());
        }
        //Debug.Log("데미지 입음");
        // 무적 시간 돌입
    }

    IEnumerator InvincibleCheck()
    {
        animator.SetBool("IsHit", true);
        isInvincible = true;
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsHit", false);
        isInvincible = false;
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
