using UnityEngine;

public class Define
{
    public static class Scene
    {
        public const string Main = "MainScene";
        public const string Load = "LoadScene";
        public const string Game = "GameScene";
    }
    public static int Default_Index = 0;
    public static int TileSize = 30;
    public static float Bullet_Speed = 15f;
    public static float Init_ItemGetRange = 3f;
    public static float AttractSpeed = 4f;

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
