using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Seal : MonoBehaviour
{
    enum State
    {
        Idle,
        Falling,
        Punching,
        Flying,
    }
    
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite punchedSprite;
    [SerializeField] float idleRotationZ = 90f;

    [SerializeField] float flySpeedX = 4f;
    [SerializeField] float flySpeedY = 10f;
    [SerializeField] float lifeTime = 1.5f;

    [SerializeField] float punchHoldTime = 0.08f;
    [SerializeField] float launchSpeed = 9f;
    [SerializeField] float launchDistance = 5f; // 1=>5
    [SerializeField] float launchStartX = 1f;
    [SerializeField] float gravityScale = 2.5f;

    int Damage;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private CapsuleCollider2D fallColl;
    private BoxCollider2D punchColl;
    State state = State.Idle;
    int dir;
    float timer;
    public bool IsActive => state != State.Idle;
    public bool CanLaunch => state == State.Falling;
    readonly HashSet<Collider2D> hitSet = new HashSet<Collider2D>();

    public void UpdateDamage(int damage)
    {
        Damage = damage;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
        fallColl = GetComponent<CapsuleCollider2D>();
        punchColl = GetComponent<BoxCollider2D>();
    }
    public void Spawn(Vector2 pos, int dir)
    {
        this.dir = dir;
        state = State.Falling;
        rb.gravityScale = gravityScale;
        timer = 0f;
        hitSet.Clear();

        transform.position = pos;
        gameObject.SetActive(true);   // 활성화 후에 물리값 세팅해야 적용됨

        ApplyVisual(false);
        SetHitbox(false);

        rb.linearVelocity = new Vector2(dir * flySpeedX, flySpeedY);
    }
    public void Launch(int launchDir,Vector2 targetpos)
    {
        if (state != State.Falling)
            return;
        dir = launchDir;
        StartCoroutine(LaunchRoutine(targetpos));
    }

    IEnumerator LaunchRoutine(Vector2 targetpos)
    {
        state = State.Punching;

        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.position = targetpos;
        launchStartX = rb.position.x;
        hitSet.Clear();               // 낙하 중 맞았던 적도 발사 타격은 다시 맞음

        ApplyVisual(true);            // 바뀐 방향 기준으로 그림 갱신
        yield return new WaitForSeconds(punchHoldTime);

        state = State.Flying;
        SetHitbox(true);
        rb.linearVelocity = new Vector2(dir * launchSpeed, 0f);
    }

    void ApplyVisual(bool punched)
    {
        bool left = dir < 0;

        if (!punched)
        {
            // 기본 물범: Z 90 고정, 오른쪽이면 FlipY 체크 / 왼쪽이면 해제
            sr.sprite = idleSprite;
            transform.localRotation = Quaternion.Euler(0f, 0f, idleRotationZ);
            sr.flipX = false;
            sr.flipY = !left;
        }
        else
        {
            // 주먹 물범: 오른쪽이면 전부 기본 / 왼쪽이면 FlipX
            sr.sprite = punchedSprite;
            transform.localRotation = Quaternion.identity;
            sr.flipY = false;
            sr.flipX = left;
        }

    }

    void SetHitbox(bool launch)
    {
        fallColl.enabled = !launch;
        punchColl.enabled = launch;
    }

    void Update()
    {
        if (state == State.Falling)
        {
            timer += Time.deltaTime;
            if (timer >= lifeTime)
                Despawn();
        }
        else if (state == State.Flying)
        {
            if (Mathf.Abs(rb.position.x - launchStartX) >= launchDistance)
                Despawn();
        }
    }

    // 자식 콜라이더에 닿아도 루트 Rigidbody 쪽으로 메시지가 옴
    void OnTriggerStay2D(Collider2D other)
    {
        if (state == State.Punching)
            return;
        if (!other.CompareTag("Enemy"))
            return;
        if (!hitSet.Add(other))
            return;

        if (other.TryGetComponent(out Enemy enemy))
            enemy.HitAction(Damage);
    }

    void Despawn()
    {
        StopAllCoroutines();
        rb.linearVelocity = Vector2.zero;
        state = State.Idle;
        gameObject.SetActive(false);
    }
    void Start()
    {
        
    }
}
