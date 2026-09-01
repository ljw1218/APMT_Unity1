using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;

    Image icon;
    TMP_Text textLevel;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        textLevel = texts[0];
    }

    void LateUpdate()
    {
        textLevel.text = $"Lv.{(level):D2}";
    }

    public void SameUpdate()
    {
        if (level == 0)
        {
            GameObject newWeapon = new GameObject();
            weapon = newWeapon.AddComponent<Weapon>();
            weapon.Init(data);
        }
        else
        {
            weapon.LevelUp(data.damages[level], data.counts[level]);
        }
    }
    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
                SameUpdate();
                break;
            case ItemData.ItemType.Range:
                SameUpdate();
                break;
            case ItemData.ItemType.Glove:
                break;
            case ItemData.ItemType.Shoe:
                break;
            case ItemData.ItemType.Heal:
                break;
        }
        level++;

        if(level == data.maxLevel)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
