using System.Collections.Generic;
using UnityEngine;

public class W_JNGPool : Weapon
{
    [SerializeField] private W_JNG JNGPrefab;
    [SerializeField] private float radius = 2f;
    [SerializeField] private float RotateSpeed = 180f;
    private List<W_JNG> JNGs = new();
    private int AttackLimit = 20;
    private int RecoveryTime = 15;
    private int InActive;
    private float time;

    protected override void Start()
    {
        base.Start();
        time = 0;
        InActive = 0;
        AddJNG();
    }

    private void Arrange()
    {
        float step = 360f / JNGs.Count;

        for(int i=0; i<JNGs.Count; i++)
        {
            float rad = step * i * Mathf.Deg2Rad;
            JNGs[i].transform.localPosition = new Vector3(Mathf.Cos(rad),Mathf.Sin(rad),0f) * radius;
            JNGs[i].transform.localRotation = Quaternion.identity;
        }
    }
    private void AddJNG()
    {
        W_JNG temp = null;

        foreach(W_JNG w in JNGs)
        {
            if(!w.gameObject.activeSelf)
            {
                temp = w;
                w.Init(this, AttackLimit);
                w.gameObject.SetActive(true);
                JNGs.Add(w);
                break;
            }
        }
        if(!temp)
        {
            temp = Instantiate(JNGPrefab, transform);
            temp.Init(this, AttackLimit);
            JNGs.Add(temp);
        }
        InActive++;
        Arrange();
    }
    private void LateUpdate()
    {
        time += Time.deltaTime;

        transform.position = player.transform.position;
        transform.Rotate(0f, 0f, -1 * RotateSpeed * Time.deltaTime);
        for(int i=0; i<JNGs.Count; i++)
        {
            if(JNGs[i].RemainHit <= 0)
            {
                InActive--;
                JNGs[i].gameObject.SetActive(false);
                time = 0;
            }
        }

        if(time >= RecoveryTime)
        {
            time = 0;
            if(InActive < count)
            {
                AddJNG();
            }
        }
    }
}
