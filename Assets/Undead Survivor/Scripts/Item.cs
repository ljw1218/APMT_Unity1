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
        level = nlevel;
    }
    public void SameWeaponUpdate(WeaponItemData data)
    {
        if (level == 0)
        {
            //수정 필요
            
        }
        else
        {
            
        }
    }

    public void SamePassiveUpdate(PassiveItemData data)
    {

    }
    public void OnClick()
    {
        if(ItemData is WeaponItemData weaponData)
        {
            SameWeaponUpdate(weaponData);
        }
        else if(ItemData is PassiveItemData passive)
        {
            SamePassiveUpdate(passive);
        }
    }
}
