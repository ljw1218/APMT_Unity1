using UnityEngine;
using System.Collections;
using UnityEngine.Diagnostics;

public class SwarmSpanwer : MonoBehaviour
{
    [SerializeField] private MonsterData data;
    [SerializeField] private float clusterRadius = 1f;
    [SerializeField] private float firstDelay = 20f;
    [SerializeField] private int count = 12;
    private float rushSpeed;
    private int poolIndex;
    
    private float interval;

    void Awake()
    {
        rushSpeed = data.Speed;
        poolIndex = data.spriteType+Define.poolFactor;
        interval = Spawner.instance.waveData.swarmSpawnTime;
    }
    void Start()
    {
        StartCoroutine(RushLoop());
    }

    IEnumerator RushLoop()
    {
        yield return new WaitForSeconds(firstDelay);
        while(true)
        {
            if (GameManager.instance.bisLive)
                SpawnCluster();
            yield return new WaitForSeconds(interval);
        }
    }

    private void SpawnCluster()
    {
        Vector3 playerPos = Player.instance.transform.position;
        BoxCollider2D AreaColl = Player.instance.AreaColl;
        Vector3 startPos = HitGeometry.GetSpawnPosition(AreaColl, 10f);
        Vector2 rushDir = (playerPos - startPos).normalized;

        for(int i=0; i<count; i++)
        {
            float r = clusterRadius * Mathf.Sqrt((i + 0.5f) / count);
            //일정한 밀도를 유지하기 위해 루트 사용, 0.5는 첫생성이 정중앙이 안되게 하여 밀도를 맞추기 위함
            float angle = i * 2.39996f; // 황금각(137.5)도를 맞추기 위한 호도법 값
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * r;
            offset += Random.insideUnitCircle * 1f; //너무 정밀함으로 인한 문제 해결을 위한 마진

            GameObject enemy = GameManager.instance.pool.GetGM(poolIndex);
            enemy.transform.position = (Vector2)startPos + offset;

            SwarmEnemy se = enemy.GetComponent<SwarmEnemy>();
            se.Init(data);
            se.StartRush(rushDir, rushSpeed);
        }
    }
}
