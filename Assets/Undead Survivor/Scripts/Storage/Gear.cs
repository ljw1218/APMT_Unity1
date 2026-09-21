using UnityEngine;

public class Gear : MonoBehaviour
{
    //public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        name = "Gear " + data.itemId;
        transform.parent = Player.instance.transform;
        transform.localPosition = Vector3.zero;


        //type = data.itemType;
        //rate = data.damages[0];
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
    }

    void RateUp()
    {

    }
}
