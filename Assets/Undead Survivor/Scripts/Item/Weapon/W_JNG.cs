using UnityEngine;

public class W_JNG : MonoBehaviour
{
    public int RemainHit {  get; private set; }
    private W_JNGPool owner;

    public void Init(W_JNGPool owner,int Limit)
    {
        this.owner = owner;
        RemainHit = Limit;
    }
}
