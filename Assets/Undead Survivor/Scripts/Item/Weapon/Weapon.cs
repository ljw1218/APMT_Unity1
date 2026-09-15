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
    protected SpriteRenderer sr;
    protected Vector2 BasePos;
    protected Vector3 mousePos;
    [SerializeField] protected Player player;
    
    protected virtual void Awake()
    {
        Damage = Data.baseDamage;
        AttackTerm = Data.attackTerm;
        AttackTime = 0;
        isAttack = false;
        transform.localScale = Vector3.one;
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
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
