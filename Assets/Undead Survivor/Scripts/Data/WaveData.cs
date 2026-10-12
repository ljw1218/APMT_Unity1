using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Object/WaveData")]
public class WaveData : ScriptableObject
{
    public int InitSpawnCount;
    public float basicSpawnTime;
    public float bossSpawnTime;
    public float swarmSpawnTime;
}
