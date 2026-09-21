using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    private Dictionary<WeaponItemData, Weapon> WeaponDict = new();

    void Awake()
    {
        instance = this;
    }
    public void AddWeapon(WeaponItemData data)
    {
        GameObject weapon = Instantiate(data.Target);
        weapon.transform.SetParent(transform,false);
        weapon.transform.localPosition = Vector3.zero;
        WeaponDict.Add(data, weapon.GetComponent<Weapon>());
    }
    public void LevelUpWeapon(WeaponItemData data)
    {
        Weapon weapon = WeaponDict[data];
        weapon.LevelUp();
    }
}
