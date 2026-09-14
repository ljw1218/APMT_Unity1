using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData/Weapon")]
public class WeaponItemData : ItemData
{
    [Header("# Type Info")]
    public WeaponType type;

    [Header("# Level Data")]
    public float attackTerm;
    public int baseDamage;
    public int baseCount;
    public int[] damages;
    public int[] counts;

    [Header("# TargetObject")]
    public GameObject Target;
}