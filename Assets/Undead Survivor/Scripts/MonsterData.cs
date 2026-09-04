using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Scriptable Object/MonsterData")]
public class MonsterData : ScriptableObject
{
    [Header("Title")]
    public string MonsterName;
    public bool isBoss;
    public int spriteType;

    [Header("Stat")]
    public float spawnTime;
    public int MaxHealth;
    public float Speed;
    public int Damage;

    [Header("Reward")]
    public CoinData Coin;

    [Header("UIData")]
    public RuntimeAnimatorController AnimCon;
}
