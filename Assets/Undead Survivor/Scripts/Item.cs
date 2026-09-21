using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private Image border;
    [SerializeField] private Image icon;
    [SerializeField] TMP_Text itemText;
    [SerializeField] TMP_Text NameText;

    private ItemData ItemData;
    private int level;

    void Awake()
    {
        Init();
    }
    void Init()
    {
        
    }

    void LateUpdate()
    {
        
    }

    public void SetItem(ItemData data,int nlevel)
    {
        ItemData = data;

        border.color = data is WeaponItemData ? Color.red : Color.green;
        icon.sprite = data.itemIcon;
        itemText.text = data.itemDesc;
        NameText.text = data.itemName;
        level = nlevel;
    }
    public void SameWeaponUpdate(WeaponItemData data)
    {
        if (level == 0)
        {
            level++;
            WeaponManager.instance.AddWeapon(data);
        }
        else
        {
            level++;
            if(level > data.maxLevel)
            {
                Debug.Log("Over the MaxLevel");
                return;
            }
            WeaponManager.instance.LevelUpWeapon(data);
            //수치변경
        }
    }

    public void SamePassiveUpdate(PassiveItemData data)
    {
        level++;

        RunTimeStat.instance.UpdateData(data, level);
    }
    public void OnClick()
    {
        LevelUp.ItemDict[ItemData] = level;
        if(ItemData is WeaponItemData weaponData)
        {
            SameWeaponUpdate(weaponData);
        }
        else if(ItemData is PassiveItemData passive)
        {
            SamePassiveUpdate(passive);
        }
        LevelUp.ItemDict[ItemData]++;
    }
}
