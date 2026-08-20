using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    public int pp;
    Rigidbody2D rigid;
    Vector3 startPos;

    public Vector3 dir { get; set; }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(startPos, transform.position) >= GameManager.instance.player.scanner.scanRange * 2)
        {
            rigid.linearVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
    public void Init(float damage,int per,Vector3 dir,int pp = 1)
    {
        this.damage = damage;
        this.per = per;

        if(per == 0)
        {
            this.pp = pp;
            this.dir = dir * Define.Bullet_Speed;
            rigid.linearVelocity = this.dir;
            startPos = transform.position;
        }
    }
    //private void OnEnable()
    //{
    //    if(per == 0)
    //    {
    //        //rigid.linearVelocity = dir;
    //        startPos = transform.position;
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || pp == 0)
            return;

        if(collision.GetComponent<Enemy>().bIsLive)
            pp--;

        if(pp == 0)
        {
            rigid.linearVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
