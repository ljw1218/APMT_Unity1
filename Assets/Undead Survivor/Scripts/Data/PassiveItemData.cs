using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData/Passive")]
public class PassiveItemData : ItemData
{
    [Header("# Type Info")]
    public PassiveType type;

    [Header("# Level Data")]
    public float[] value;
}