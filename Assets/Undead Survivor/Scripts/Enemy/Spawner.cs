using NUnit.Framework;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    [SerializeField] public WaveData waveData;
    [SerializeField] public MonsterData[] monsterData;
    private int level;
    private float timer;
    private int totalWeight = 0;
    private int levelBoss = 0;
    private int basicLevelStart = 1;
    // Update is called once per frame

    void Awake()
    {
        level = basicLevelStart;
        instance = this;
    }
    void Start()
    {
        for(int i= basicLevelStart; i<monsterData.Length; i++)
        {
            totalWeight += monsterData[i].weight;
        }
        StartCoroutine(SpawnBossTimer());
        for(int i=0; i<waveData.InitSpawnCount; i++)
        {
            Spawn(basicLevelStart);
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= waveData.basicSpawnTime)
        {
            timer = 0;
            int rand = Random.Range(0, totalWeight);
            int index = basicLevelStart;
            for (int i = basicLevelStart; i < monsterData.Length; i++)
            {
                if (monsterData[i].weight < rand)
                {
                    index = i;
                    break;
                }
                rand -= monsterData[i].weight;
            }
            Spawn(index);
        }
    }
    IEnumerator SpawnBossTimer()
    {
        yield return new WaitForSeconds(GameManager.instance.nBossSpawnTime);
        SpawnBoss();
    }
    void SpawnBoss()
    {
        Spawn(levelBoss);
    }
    void Spawn(int index)
    {
        int poolIndex = index + Define.poolFactor;
        Vector3 spawnPos = HitGeometry.GetSpawnPosition(Player.instance.AreaColl, 3f);

        GameObject enemy = GameManager.instance.pool.GetGM(poolIndex);
        enemy.transform.position = spawnPos;
        enemy.GetComponent<Enemy>().Init(monsterData[index]);
        
    }
    
    //Vector3 GetSpawnPosition(BoxCollider2D AreaColl, float margin)
    //{
    //    Vector3 offset = Vector3.zero;

    //    Bounds bounds = AreaColl.bounds;
    //    float dist = bounds.extents.x + margin;
    //    float angle = Random.Range(0f, Mathf.PI * 2f);

    //    offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * dist;

    //    return (Vector3)bounds.center + offset;
    //}
}
