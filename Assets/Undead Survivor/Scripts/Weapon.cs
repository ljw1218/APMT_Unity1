using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    float timer;
    Player player;

    void Start()
    {
        Init();
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
                }
                break;

            default:
                break;
        }

        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();

    }

    public void Init()
    {
        player = GetComponentInParent<Player>();
        switch(id)
        {
            case 0:
                speed = 150;
                Batch();
                break;

            case 1:
                speed = 0.3f;
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

            bullet.GetComponent<Bullet>().Init(damage,Define.Type_Infinity,Vector3.zero);
            
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;
    }
}
