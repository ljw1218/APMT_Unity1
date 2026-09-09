using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum PassiveType
    {
        Attack,
        MoveSpeed,
        AttackSpeed,
    }
    public enum WeaponType
    {
        Melee,
        Range,
    }
    [Header("# Main Info")]
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;
    public int maxLevel;
}
