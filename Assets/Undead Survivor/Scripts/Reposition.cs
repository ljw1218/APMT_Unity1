using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Dictionary<Vector2Int, GameObject> activeTiles = new();
    //Collider2D coll;
    Player player;
    Vector2Int CurrentCenter;

    void Awake()
    {
        //coll = GetComponent<Collider2D>();
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            Vector2Int coordinate = new Vector2Int(
                Mathf.RoundToInt(child.position.x / Define.TileSize),
                Mathf.RoundToInt(child.position.y / Define.TileSize)
            );

            activeTiles[coordinate] = child.gameObject;
        }
    }

    void Start()
    {
        player = GameManager.instance.player;
        CurrentCenter = new Vector2Int(Mathf.RoundToInt(player.transform.position.x),Mathf.RoundToInt(player.transform.position.y));
    }
    void Update()
    {
        Vector2Int newCenter = new Vector2Int(
            Mathf.RoundToInt(player.transform.position.x / Define.TileSize),
            Mathf.RoundToInt(player.transform.position.y / Define.TileSize)
        );

        if (newCenter != CurrentCenter)
        {
            CurrentCenter = newCenter;
            UpdateTiles(CurrentCenter);
        }
    }
    void UpdateTiles(Vector2Int newCenter)
    {
        HashSet<Vector2Int> required = new HashSet<Vector2Int>();
        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
                required.Add(newCenter + new Vector2Int(x, y));

        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (var coordinate in activeTiles.Keys)
        {
            if(!required.Contains(coordinate))
                toRemove.Add(coordinate);
        }

        List<Vector2Int> toAdd = new List<Vector2Int>();
        foreach(var coordinate in required)
        {
            if(!activeTiles.ContainsKey(coordinate))
                toAdd.Add(coordinate);
        }

        for(int i=0; i<toRemove.Count; i++)
        {
            GameObject tile = activeTiles[toRemove[i]];
            activeTiles.Remove(toRemove[i]);

            Vector2Int nCoordinate = toAdd[i];
            tile.transform.position = new Vector3(nCoordinate.x * Define.TileSize, nCoordinate.y * Define.TileSize, 0);
            activeTiles[nCoordinate] = tile;
        }
    }
    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag("Area"))
    //        return;

    //    Vector3 playerPos = GameManager.instance.player.transform.position;
    //    Vector3 myPos = transform.position;
    //    //float diffX = Mathf.Abs(playerPos.x - myPos.x);
    //    //float diffY = Mathf.Abs(playerPos.y - myPos.y);

    //    Vector3 playerDir = GameManager.instance.player.inputVec;
    //    //float dirX = playerDir.x < 0 ? -1 : 1;
    //    //float dirY = playerDir.y < 0 ? -1 : 1;

    //    float dirX = playerPos.x - myPos.x;
    //    float dirY = playerPos.y - myPos.y;

    //    float diffX = Mathf.Abs(dirX);
    //    float diffY = Mathf.Abs(dirY);

    //    dirX = dirX > 0 ? 1 : -1;
    //    dirY = dirY > 0 ? 1 : -1;

    //    switch (transform.tag)
    //    {
    //        case "Ground":
                
    //            if (diffX > diffY)
    //            {
    //                transform.Translate(Vector3.right * dirX * 60);
                    
    //            }
    //            else if (diffX < diffY)
    //            {
    //                transform.Translate(Vector3.up * dirY * 60);
                    
    //            }
    //            break;

    //        case "Area":
    //            break;

    //        case "Enemy":
    //            if (coll.enabled)
    //            {
    //                transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
    //            }
    //            break;
    //    }
    //}
}
