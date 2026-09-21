using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_JNGPool : Weapon
{
    [SerializeField] private W_JNG JNGPrefab;
    [SerializeField] private float radius = 1.5f;
    [SerializeField] private float RotateSpeed = 180f;
    private List<W_JNG> JNGs = new();
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
        for(int i=0; i<count; i++)
        {
            W_JNG JNG = Instantiate(JNGPrefab);
            JNG.transform.SetParent(transform,false);
            JNGs.Add(JNG);
            JNGs[i].InitJNG(this, AttackLimit);
        }
        Arrange();
    }
    protected override void UpdateData()
    {
        base.UpdateData();
        int orgcnt = count;
        count = Data.counts[level];
        AddJNG(count - orgcnt);
        Arrange();
    }
    private void AddJNG(int ToAdd)
    {
        for (int i = 0; i < ToAdd; i++)
        {
            W_JNG JNG = Instantiate(JNGPrefab);
            JNG.transform.SetParent(transform, false);
            JNGs.Add(JNG);
            JNGs[JNGs.Count - 1].InitJNG(this, AttackLimit);
        }
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
        for(int i=0; i<JNGs.Count; i++)
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
