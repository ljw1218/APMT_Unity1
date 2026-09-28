using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData/Active")]
public class ActiveItemData : ItemData
{
    public WeaponItemData ActiveTarget;

}
