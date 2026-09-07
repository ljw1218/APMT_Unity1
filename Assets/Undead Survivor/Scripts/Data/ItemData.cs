using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Melee,Range,Glove,Shoe,Heal
    }
    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;
    public int maxLevel;

    [Header("# Level Data")]
    public int baseDamage;
    public int baseCount;
    public int[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
}
