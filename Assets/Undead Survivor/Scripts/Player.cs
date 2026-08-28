using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 6f;
    public Scanner scanner;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    //public Weapon StartWeapon;
    //public ItemData StartItemData;

    Animator anim;
    void Start()
    {
        //GameObject weapon = new GameObject();
        //StartWeapon = weapon.AddComponent<Weapon>();
        //StartWeapon.Init(StartItemData);
        //float fStartDamage = StartItemData.baseDamage + StartItemData.damages[0];
        //int fStartCount = StartItemData.baseCount + StartItemData.counts[0];

        //StartWeapon.LevelUp(fStartDamage,fStartCount);
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
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
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
}
