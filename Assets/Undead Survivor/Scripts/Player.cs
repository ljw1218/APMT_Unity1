using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Vector2 inputVec;
    RunTimeStat Rstat;
    PlayerStat Pstat;
    public Scanner scanner;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    public int level,exp;
    public float hp;
    public int nextExp;

    //public Weapon StartWeapon;
    //public ItemData StartItemData;

    Animator anim;

    void InitStat()
    {
        nextExp = Pstat.BaseData.ExpTerm;
        Pstat.Init();
        Pstat.RecalculateStats();
        hp = Pstat.MaxHealth;
        level = 0;
    }
    public void GetExp(int ExpData)
    {
        exp += ExpData;

        if (exp >= nextExp)
        {
            level++;
            exp = 0;
            GameManager.instance.uiLevelUp.Show();
        }
    }
    void Start()
    {
        Rstat = RunTimeStat.instance;
        Pstat = PlayerStat.instance;
        InitStat();
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
        Vector2 nextVec = inputVec.normalized * Pstat.Move_Speed * Time.fixedDeltaTime;
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
        hp -= (int)Time.deltaTime * Edamage;

        if(hp <= 0.001)
        {
            for(int i=2; i<transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            GameManager.instance.uiDead.Show();
            anim.SetTrigger("Dead");
        }
    }

    
}
