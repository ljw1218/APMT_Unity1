using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Object/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int MaxHealth;
    public int Damage;
    public float Move_Speed;
    public float Attack_Speed;
    public float Attack_Range;
    public float Critical_Pro;
    public float Critical_Dam;
    public float ItemGetRange;
    public int ExpTerm;

}
