using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    const string strIcon = "Icon";
    public ItemData ItemData;
    public int level {  get; private set; }
    public Weapon weapon { get; private set; }

    Image icon;
    TMP_Text textLevel;

    void Awake()
    {
        Init();
    }
    void Init()
    {
        //icon = GetComponentsInChildren<Image>()[1];
        icon = transform.Find(strIcon).GetComponent<Image>();
        icon.sprite = ItemData.itemIcon;

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        textLevel = texts[0];
    }

    void LateUpdate()
    {
        textLevel.text = $"Lv.{(level):D2}";
    }

    public void SameWeaponUpdate(WeaponItemData data)
    {
        if (level == 0)
        {
            //수정 필요
            GameObject newWeapon = new GameObject();
            weapon = newWeapon.AddComponent<Weapon>();
            //weapon.Init(data);
        }
        else
        {
            //weapon.LevelUp(data.damages[level], data.counts[level]);
        }
    }

    public void SamePassiveUpdate(PassiveItemData data)
    {

    }
    public void OnClick()
    {
        int maxLevel = 3;
        if(ItemData is WeaponItemData weaponData)
        {
            SameWeaponUpdate(weaponData);
            maxLevel = weaponData.maxLevel;
        }
        else if(ItemData is PassiveItemData passive)
        {
            maxLevel = passive.maxLevel;
        }

        level++;

        if(level == maxLevel)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
