using UnityEngine;

public class W_JNG : W_JNGPool
{
    public int RemainHit {  get; private set; }
    private W_JNGPool owner;
    public bool isAlive;

    public void InitJNG(W_JNGPool owner,int Limit)
    {
        this.owner = owner;
        RemainHit = Limit;
    }
}
