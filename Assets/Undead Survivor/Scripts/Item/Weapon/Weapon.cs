using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponItemData Data;
    public int Damage { get; private set; }
    public float AttackTerm { get; private set; }
    private float AttackTime;
    private bool isAttack;
    protected int level;
    protected int count;
    protected SpriteRenderer sr;
    protected Vector2 BasePos;
    protected Vector3 mousePos;
    protected Player player;
    private RunTimeStat Rstat;
    
    protected virtual void Awake()
    {
        isAttack = false;
        transform.localScale = Vector3.one;
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        level = 1;
        count = Data.baseCount;
    }

    protected virtual void Start()
    {
        player = GameManager.instance.player;
        Rstat = RunTimeStat.instance;
        Damage = Data.baseDamage * Rstat.DamageScale;
        AttackTerm = Data.attackTerm * (1 / Rstat.Attack_SpeedScale);
        AttackTime = 0;
    }
    protected void LevelUp(int nlevel)
    {
        level = nlevel;
    }    
    protected IEnumerator Attack()
    {
        isAttack = true;
        sr.enabled = true;
        BasePos = player.transform.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        yield return StartCoroutine(AttackRoutine());
        isAttack = false;
        sr.enabled = false;
    }

    protected virtual void Update()
    {
        if (isAttack)
            return;
        AttackTime += Time.deltaTime;
        if (AttackTime >= AttackTerm)
        {
            AttackTime = 0;
            StartCoroutine(Attack());
        }
    }
    protected virtual IEnumerator AttackRoutine()
    {
        yield return null;
    }
}
