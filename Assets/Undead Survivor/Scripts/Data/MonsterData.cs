using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Scriptable Object/MonsterData")]
public class MonsterData : ScriptableObject
{
    public enum SpawnPatternType
    {
        Chase,
        Swarm,
        Ring,
        Wall,
    }

    [Header("Title")]
    public string MonsterName;
    public bool isBoss;
    public int spriteType;
    public SpawnPatternType patternType;

    [Header("Stat")]
    public float spawnTime;
    public int MaxHealth;
    public float Speed;
    public int Damage;
    public int count;

    [Header("Reward")]
    public CoinData Coin;

    [Header("UIData")]
    public RuntimeAnimatorController AnimCon;
}
