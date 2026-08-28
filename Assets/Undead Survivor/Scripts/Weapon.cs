using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public int damage;
    public int count;
    public float speed;
    float timer;
    int pp;
    Player player;

    List<GameObject> Bulletpool = new List<GameObject>();
    void Awake()
    {
        player = GameManager.instance.player;
    }
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            case 1:
                timer += Time.deltaTime;
                if(timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;

            default:
                break;
        }

        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(3, 1, 0);
        }
    }

    public void LevelUp(int damage, int count,int pp = 0)
    {
        this.damage += damage;
        this.count += count;
        this.pp += pp; 

        if (id == 0)
            Batch();
        else if (id == 1)
            UpdateFire();
    }

    public void Init(ItemData data)
    {
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for(int i=0; i < GameManager.instance.pool.prefabs.Length; i++)
        {
            if(data.projectile == GameManager.instance.pool.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }

        switch(id)
        {
            case 0:
                speed = 150;
                Batch();
                break;

            case 1:
                speed = 1f;
                pp = 1;
                UpdateFire();
                break;

            default:
                break;
        }
    }

    void Batch()
    {
        for(int i=0; i<count; i++)
        {
            Transform bullet;
            if (i >= transform.childCount)
            {
                bullet = GameManager.instance.pool.GetGM(prefabId).transform;
                bullet.parent = transform;
            }
            else
            {
                bullet = transform.GetChild(i);
                
            }
            //bullet.position = transform.position + new Vector3(0, 1.2f, 0);

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up*1.2f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage,Define.WeaponType.Infinity,Vector3.zero);
            
        }
    }
    
    void UpdateFire()
    {
        GameManager.instance.pool.UpdatePool(prefabId, damage, count, pp);
    }
    
    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = (targetPos - transform.position).normalized;

        //Transform bullet = GameManager.instance.pool.GetGM(prefabId).transform;
        for (int i = 0; i < count; i++)
        {
            Transform bullet = GameManager.instance.pool.GetGM(prefabId).transform;
            Vector3 rotatedDir = Quaternion.Euler(0, 0, -15f * i) * dir;

            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<Bullet>().Init(damage, Define.WeaponType.Bullet, rotatedDir, pp);
            bullet.parent = transform;
        }
    }
}
