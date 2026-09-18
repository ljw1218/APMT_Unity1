using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_JNGPool : Weapon
{
    [SerializeField] private W_JNG JNGPrefab;
    [SerializeField] private float radius = 2f;
    [SerializeField] private float RotateSpeed = 180f;
    private W_JNG[] JNGs;
    private int AttackLimit = 20;
    private int RecoveryTime = 15;
    private Queue<int> QInactive = new();
    private float time;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    protected override void Start()
    {
        base.Start();
        time = 0;
    }

    private void Init()
    {
        JNGs = new W_JNG[count];
        for(int i=0; i<JNGs.Length; i++)
        {
            W_JNG JNG = Instantiate(JNGPrefab);
            JNG.transform.parent = transform;
            JNGs[i] = JNG;
            JNGs[i].InitJNG(this, AttackLimit);
        }
        Arrange();
    }
    protected override void UpdateData()
    {
        base.UpdateData();
        count = Data.counts[level];
        Arrange();
    }
    private void Arrange()
    {
        float step = 360f / JNGs.Length;

        for(int i=0; i<JNGs.Length; i++)
        {
            float rad = step * i * Mathf.Deg2Rad;
            JNGs[i].transform.localPosition = new Vector3(Mathf.Cos(rad),Mathf.Sin(rad),0f) * radius;
            JNGs[i].transform.localRotation = Quaternion.identity;
        }
    }

    //private void AddJNG()
    //{
    //    W_JNG temp = null;

    //    foreach(W_JNG w in JNGs)
    //    {
    //        if(!w.gameObject.activeSelf)
    //        {
    //            temp = w;
    //            w.Init(this, AttackLimit);
    //            w.gameObject.SetActive(true);
    //            JNGs.Add(w);
    //            break;
    //        }
    //    }
    //    if(!temp)
    //    {
    //        temp = Instantiate(JNGPrefab, transform);
    //        temp.Init(this, AttackLimit);
    //        JNGs.Add(temp);
    //    }
    //    InActive++;
    //    Arrange();
    //}
    private void Recovery()
    {
        int index = QInactive.Dequeue();
        JNGs[index].gameObject.SetActive(true);
    }
    private void LateUpdate()
    {
        time += Time.deltaTime;

        transform.position = player.transform.position;
        transform.Rotate(0f, 0f, -1 * RotateSpeed * Time.deltaTime);
        for(int i=0; i<JNGs.Length; i++)
        {
            if(JNGs[i].RemainHit <= 0)
            {
                if (JNGs[i].isAlive)
                {
                    QInactive.Enqueue(i);
                    JNGs[i].gameObject.SetActive(false);
                    time = 0;
                }
            }
        }

        if(time >= RecoveryTime)
        {
            time = 0;
            if(QInactive.Count > 0)
            {
                Recovery();
            }
        }
    }
}
