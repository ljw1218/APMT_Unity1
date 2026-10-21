using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;

public class SwarmEnemy : Enemy
{
    [SerializeField] private float maxLifeTime = 12f;
    public bool isRushing {  get; private set; }
    private Vector2 dir;
    private bool enteredScreen;
    private float timer;
    private float margin = 0.15f;
    private SpriteRenderer sr;

    private Color32 colorHit = new Color32(255, 167, 167, 255);
    Camera cam;

    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
    }
    public void StartRush(Vector2 dir,float rushSpeed)
    {
        this.dir = dir;
        speed = rushSpeed;
        enteredScreen = false;
        timer = 0f;
        isRushing = true;

        GetComponent<SpriteRenderer>().flipX = dir.x < 0;
    }

    private void FixedUpdate()
    {
        if (!isRushing)
            return;
        rigid.MovePosition(rigid.position + dir * speed * Time.fixedDeltaTime);
        rigid.linearVelocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        return;
    }
    
    private void Update()
    {
        if (!isRushing)
            return;

        timer += Time.deltaTime;
        Vector3 vp = cam.WorldToViewportPoint(transform.position);
        bool inScreen = vp.x > 0 && vp.x < 1 && vp.y > 0 && vp.y < 1;

        if(inScreen)
        {
            enteredScreen = true;
            return;
        }

        bool farOut = vp.x < -1 * margin || vp.x > 1 + margin || vp.y < -1 * margin || vp.y > 1 + margin;
        if ((enteredScreen && farOut) || timer > maxLifeTime)
            Despawn();
    }

    public override void HitAction(int rDamage)
    {
        health -= rDamage;

        if (health <= 0)
        {
            bIsLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            Despawn();
            //GameManager.instance.kill++;
            //GameManager.instance.GetExp(coindata);
        }
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        sr.color = colorHit;
        yield return new WaitForSeconds(0.15f);
        sr.color = Color.white;
    }
    private void Despawn()
    {
        isRushing = false;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        isRushing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!GameManager.instance.bisLive)
                return;

            Player.instance.hp -= Damage;
            Player.instance.DeadSequence();
        }
    }

}
