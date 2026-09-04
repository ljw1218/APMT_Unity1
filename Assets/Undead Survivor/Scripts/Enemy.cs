using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int maxHealth;
    public int health;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    public CoinData coindata;
    Vector2 dirVec;

    public bool bIsLive;
    public bool bIsBoss;

    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer spriter;
    Animator anim;
    
    void Awake()
    {
        rigid = GetComponent <Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!bIsLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        if (!bIsLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
    }
    
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        bIsLive = true;
        health = maxHealth;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
    }

    public void Init(MonsterData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.Speed;
        maxHealth = data.MaxHealth;
        health = data.MaxHealth;
        bIsBoss = data.isBoss;
        coindata = data.Coin;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !bIsLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if(health <= 0)
        {
            bIsLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            CreateCoin();
            //GameManager.instance.kill++;
            //GameManager.instance.GetExp(coindata);
        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }
    void CreateCoin()
    {
        GameObject coin = GameManager.instance.pool.GetGM((int)Define.PoolType.Coin);
        coin.transform.position = transform.position;
        CoinPickup pickup = coin.GetComponent<CoinPickup>();
        pickup.Init(coindata);
    }
    IEnumerator KnockBack()
    {
        yield return new WaitForFixedUpdate();
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = (transform.position - playerPos).normalized;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
        if(bIsBoss == true)
        {
            GameManager.instance.bisClear = true;
            GameManager.instance.uiClear.Show();
        }
    }
}
