using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponItemData Data;
    public int Damage { get; private set; }
    public float AttackTerm { get; private set; }
    
    void Awake()
    {
        Damage = Data.baseDamage;
        AttackTerm = Data.attackTerm;
    }

    protected virtual void Attack()
    {

    }
}
