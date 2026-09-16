using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Dictionary<WeaponItemData, Weapon> WeaponDict = new();
    public void AddWeapon(WeaponItemData data)
    {
        GameObject weapon = Instantiate(data.Target);
        weapon.transform.localPosition = Vector3.zero;
        WeaponDict.Add(data, weapon.GetComponent<Weapon>());
    }
}
