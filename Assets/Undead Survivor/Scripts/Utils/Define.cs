using UnityEngine;

public static class Define
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
    public static float AttractSpeed = 4f;
    public static string strRushEnemy = "RushEnemy";
    public static int poolFactor = 1;
    public static int displayWindowFull_Index = 4;
    public static int displayFull_Index = 5;
    public static string strEffectRatio = "EffectRatio";
    public static string strED = "ED";
    public static string strPD = "PD";

    public enum WeaponType
    {
        Infinity = -1,
        Bullet = 0,
    }

    public enum PoolType
    {
        Coin = 0,
        MonsterBoss,
        MonsterOne,
        MonsterTwo,
        MonsterRush,
        
    }
}
