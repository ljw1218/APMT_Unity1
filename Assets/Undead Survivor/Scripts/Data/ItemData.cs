using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum PassiveType
    {
        Attack,
        MoveSpeed,
        AttackSpeed,
        Potion,
    }
    public enum WeaponType
    {
        Default,
        Rotate,
    }
    [Header("# Main Info")]
    public int itemId;
    public string itemName;
    [TextArea(1,6)]
    public string itemDesc;
    public Sprite itemIcon;
    public int maxLevel;
}
