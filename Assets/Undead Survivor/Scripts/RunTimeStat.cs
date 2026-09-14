using UnityEngine;

public class RunTimeStat : MonoBehaviour
{
    public int MaxHealthScale { get; private set; }
    public int DamageScale { get; private set; }
    public float Move_SpeedScale { get; private set; }
    public float Attack_SpeedScale { get; private set; }
    public float Attack_RangeScale { get; private set; }
    public float Critical_Pro_Added { get; private set; }
    public float Critical_DamScale { get; private set; }
    public float ItemGetRangeScale { get; private set; }

    void Awake()
    {
        MaxHealthScale = 1;
        DamageScale = 1;
        Move_SpeedScale = 1;
        Attack_SpeedScale = 1;
        Attack_RangeScale = 1;
        Critical_Pro_Added = 0;
        Critical_DamScale = 1;
        ItemGetRangeScale = 1;
    }
}
