using UnityEngine;

public class AttractArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<CoinPickup>(out var coin))
        {
            coin.StartAttract();
        }
    }
}
