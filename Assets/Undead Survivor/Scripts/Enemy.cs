using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float maxHealth;
    public float health;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    Vector2 dirVec;

    public bool bIsLive;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    
    void Awake()
    {
        rigid = GetComponent < Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !bIsLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if(health < 0)
        {
            Dead();
        }
        else
        {
            anim.SetTrigger("Hit");
        }
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
        bIsLive = false;
    }
}
