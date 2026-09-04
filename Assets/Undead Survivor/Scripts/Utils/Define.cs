using UnityEngine;

public class Define : MonoBehaviour
{
    public static int TileSize = 30;
    public static float Bullet_Speed = 15f;
    public static float Init_ItemGetRange = 3f;

    public enum WeaponType
    {
        Infinity = -1,
        Bullet = 0,
    }

    public enum PoolType
    {
        Monster = 0,
        Shovel,
        Bullet,
        Coin,
    }
}
