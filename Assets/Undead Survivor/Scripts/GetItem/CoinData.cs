using UnityEngine;

[CreateAssetMenu(fileName = "Coin", menuName = "Scriptable Object/CoinData")]
public class CoinData : ScriptableObject
{
    public string CoinName;
    public Sprite icon;
    public int exp;
    public int gold;
}
