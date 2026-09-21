using UnityEngine;

public class W_JNG : MonoBehaviour
{
    public int RemainHit {  get; private set; }
    private W_JNGPool owner;
    public bool isAlive;

    public void InitJNG(W_JNGPool owner,int Limit)
    {
        this.owner = owner;
        RemainHit = Limit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.HitAction(owner.Damage);
        }
    }
}
