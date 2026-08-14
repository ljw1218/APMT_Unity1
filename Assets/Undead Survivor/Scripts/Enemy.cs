using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2;
    public Rigidbody2D target;
    Vector2 dirVec;

    bool bIsLive;

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
        dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        if (dirVec.x != 0)
        {
            spriter.flipX = dirVec.x < 0;
        }
    }
}
