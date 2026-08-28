using NUnit.Framework;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public SpawnData[] spawnData;
    Transform AreaTransform;

    int level;
    float timer;
    // Update is called once per frame

    void Awake()
    {
        AreaTransform = GameObject.FindGameObjectWithTag("Area").transform;
    }
    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 15f),spawnData.Length-1);

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        Vector3 spawnPos = GetSpawnPosition(AreaTransform.GetComponent<BoxCollider2D>(), 5f);

        GameObject enemy = GameManager.instance.pool.GetGM(0);
        enemy.transform.position = spawnPos;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
        
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

[Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;

    
}