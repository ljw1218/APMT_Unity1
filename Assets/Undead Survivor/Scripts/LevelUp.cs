using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    //Item[] items;
    public List<ItemData> ItemList;

    public static Dictionary<ItemData, int> ItemDict = new();

    [SerializeField] private Item[] itemslot;
    [SerializeField] private ItemData ItemPostion;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        foreach (ItemData data in ItemList)
        {
            if (data is WeaponItemData wdata && wdata.type == ItemData.WeaponType.Default)
            {
                ItemDict[data] = 1;
            }
            else
                ItemDict[data] = 0;
        }
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }


    void Next()
    {
        // 1. 모든 아이템 비활성화
        //foreach (Item item in items)
        //{
        //    item.gameObject.SetActive(false);
        //}

        // 2. 그 중에서 랜덤 3개 아이템 활성화
        List<ItemData> CopyList = new List<ItemData>();
        List<ItemData> RanItem = new List<ItemData>();

        for(int i=0; i<ItemList.Count; i++)
        {
            if (ItemDict[ItemList[i]] == ItemList[i].maxLevel)
            {
                continue;
            }
            CopyList.Add(ItemList[i]);
        }
        if (CopyList.Count < 3)
        {
            for (int i = 0; i < CopyList.Count; i++)
                RanItem.Add(CopyList[i]);
            for (int i=CopyList.Count; i<3; i++)
            {
                RanItem.Add(ItemPostion);
            }
        }
        else
        {
            int[] ToCheck = { -1, -1, -1 };
            while (RanItem.Count < 3)
            {
                int rand = Random.Range(0, CopyList.Count);
                bool bDup = false;
                foreach(int n in ToCheck)
                {
                    if(rand == n)
                    {
                        bDup = true;
                        break;
                    }
                }
                if (!bDup)
                {
                    ToCheck[RanItem.Count] = rand;
                    RanItem.Add(CopyList[rand]);
                }
            }
        }

        for(int i=0; i<3; i++)
        {
            itemslot[i].SetItem(RanItem[i], ItemDict[RanItem[i]]);
        }

        // 3. 만렙 아이템의 경우 나오지 않음 , 그외가 3이 안될경우 소비 아이템
    }
}
