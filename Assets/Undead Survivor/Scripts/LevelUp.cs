using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
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

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        // 1. 모든 아이템 비활성화
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // 2. 그 중에서 랜덤 3개 아이템 활성화
        List<Item> CopyList = new List<Item>();
        List<Item> RanItem = new List<Item>();

        for(int i=0; i<items.Length; i++)
        {
            if (items[i].level == items[i].data.maxLevel)
            {
                continue;
            }
            CopyList.Add(items[i]);
        }
        if (CopyList.Count < 3)
        {
            for (int i = 0; i < CopyList.Count; i++)
                RanItem.Add(CopyList[i]);
        }
        else
        {
            while (RanItem.Count < 3)
            {
                int rand = Random.Range(0, CopyList.Count);
                RanItem.Add(CopyList[rand]);
            }
        }

        for(int i=0; i<RanItem.Count; i++)
        {
            RanItem[i].gameObject.SetActive(true);
        }

        // 3. 만렙 아이템의 경우 나오지 않음 , 그외가 3이 안될경우 소비 아이템
    }
}
