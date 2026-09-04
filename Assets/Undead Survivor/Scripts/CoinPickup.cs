using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    CoinData data;

    public void Init(CoinData data)
    {
        this.data = data;
        GetComponent<SpriteRenderer>().sprite = data.icon;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        GameManager.instance.GetExp(data.exp);
        gameObject.SetActive(false);
    }
}
