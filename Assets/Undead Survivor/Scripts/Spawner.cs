using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnData[] spawnData;

    int level;
    float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);

        if(timer > (level == 0 ? 0.5f : 1f))
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        Transform enemy = GameManager.instance.pool.GetGM(level).transform;

    }
}

[Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;

    public SpawnData(){
        spriteType = 0;
        spawnTime = 0;
        health = 0;
        speed = 0f;
    }
}