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
        level = 1;
        count = Data.baseCount;
    }

    protected virtual void Start()
    {
        player = Player.instance;
        Rstat = RunTimeStat.instance;
        Damage = Data.baseDamage * Rstat.DamageScale;
        AttackTerm = Data.attackTerm * (1 / Rstat.Attack_SpeedScale);
        AttackTime = 0;
    }
    public void LevelUp()
    {
        level++;
        UpdateData();
    }
    protected virtual void UpdateData()
    {
        Damage = Data.damages[level] * Rstat.DamageScale;
        AttackTerm = Data.attackTerm * (1 / Rstat.Attack_SpeedScale);
    }
    protected IEnumerator Attack()
    {
        isAttack = true;
        BasePos = player.transform.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        yield return StartCoroutine(AttackRoutine());
        isAttack = false;
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
