using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    CoinData data;
    bool isAttracting;


    public void Init(CoinData data)
    {
        this.data = data;
        isAttracting = false;
        GetComponent<SpriteRenderer>().sprite = data.icon;
    }

    public void StartAttract()
    {
        isAttracting = true;
    }

    public void OnPickedUp()
    {
        GameManager.instance.GetExp(data.exp);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        GameManager.instance.GetExp(data.exp);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (isAttracting)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                GameManager.instance.player.transform.position, Define.AttractSpeed * Time.deltaTime);
        }
    }
}
