using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    PlayerStat stat;
    public Scanner scanner;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    //public Weapon StartWeapon;
    //public ItemData StartItemData;

    Animator anim;
    void Start()
    {

    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * stat.Move_Speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);
        if(inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.bisLive)
            return;

        int Edamage = collision.transform.GetComponent<Enemy>().Damage;
        GameManager.instance.health -= Time.deltaTime * Edamage;

        if(GameManager.instance.health <= 0)
        {
            for(int i=2; i<transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                GameManager.instance.uiDead.Show();
            }

            anim.SetTrigger("Dead");
        }
    }

    
}
