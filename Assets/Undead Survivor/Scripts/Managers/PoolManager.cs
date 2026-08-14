using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    Transform AreaTransform;

    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        AreaTransform = GameObject.FindGameObjectWithTag("Area").transform;

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }

    }

    public GameObject GetGM(int index)
    {
        GameObject temp = null;
        Vector3 spawnPos = GetSpawnPosition(AreaTransform.GetComponent<BoxCollider2D>(), 5f);

        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                temp = item;
                temp.transform.position = spawnPos;
                temp.SetActive(true);
                break;
            }
        }

        if(!temp)
        {
            temp = Instantiate(prefabs[index], spawnPos,Quaternion.identity);
            pools[index].Add(temp);
        }

        return temp;
    }

    Vector3 GetSpawnPosition(BoxCollider2D AreaColl, float margin)
    {
        Vector3 offset = Vector3.zero;

        Bounds bounds = AreaColl.bounds;
        float dist = bounds.extents.x + margin;
        float angle = Random.Range(0f, Mathf.PI * 2f);

        offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * dist;

        return (Vector3)bounds.center + offset;
    }

}
