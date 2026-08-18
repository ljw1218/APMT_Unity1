using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

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
                break;

            default:
                break;
        }
    }

    public void Init()
    {
        switch(id)
        {
            case 0:
                speed = 150;
                Batch();
                break;

            case 1:
                break;

            default:
                break;
        }
    }

    void Batch()
    {
        for(int i=0; i<count; i++)
        {
            Transform bullet = GameManager.instance.pool.GetGM(prefabId).transform;
            bullet.parent = transform;
            //bullet.position = transform.position + new Vector3(0, 1.2f, 0);

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up*1.2f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage,Define.Type_Infinity);
            
        }
    }
}
