using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public PlayerData BaseData;
    public RunTimeStat RunStat;
    
    public int MaxHealth {  get; private set; }
    public int Damage { get; private set; }
    public float Move_Speed { get; private set; }
    public float Attack_Speed { get; private set; }
    public float Attack_Range { get; private set; }
    public float Critical_Pro { get; private set; }
    public float Critical_Dam { get; private set; }
    public float ItemGetRange { get; private set; }

    void Start()
    {
        
    }
    public void RecalculateStats()
    {
        MaxHealth = RunStat.MaxHealthScale * BaseData.MaxHealth;
        Damage = RunStat.DamageScale * BaseData.Damage;
        Move_Speed = RunStat.Move_SpeedScale * BaseData.Move_Speed;
        Attack_Speed = RunStat.Attack_SpeedScale * BaseData.Attack_Speed;
        Attack_Range = RunStat.Attack_RangeScale * BaseData.Attack_Range;
        Critical_Pro = RunStat.Critical_Pro_Added + BaseData.Critical_Pro;
        Critical_Dam = RunStat.Critical_DamScale * BaseData.Critical_Dam;
        ItemGetRange = RunStat.ItemGetRangeScale * BaseData.ItemGetRange;
    }
}
