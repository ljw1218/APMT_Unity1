using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }

    }

    public GameObject GetGM(int index)
    {
        GameObject temp = null;
        
        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                temp = item;
                temp.SetActive(true);
                break;
            }
        }

        if(!temp)
        {
            temp = Instantiate(prefabs[index],Vector3.zero,Quaternion.identity);
            pools[index].Add(temp);
        }

        return temp;
    }

}
