using UnityEngine;
using UnityEngine.UI;

public class MainStar : MonoBehaviour
{
    public float minScale = 0.7f;
    public float maxScale = 1.2f;
    public float speed = 2f;
    public float randomOffset;

    private Image img;
    private Vector3 baseScale;

    void Awake()
    {
        img = GetComponent<Image>();
        baseScale = transform.localScale;
        randomOffset = Random.Range(0f, 100f);
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        float t = Mathf.PingPong((Time.unscaledTime + randomOffset) * speed, 1f);
        float scale = Mathf.Lerp(minScale, maxScale, t); // 퍼센트만큼 스케일변화
        transform.localScale = baseScale * scale;

        Color c = img.color;
        c.a = Mathf.Lerp(0.3f, 1f, t);
        img.color = c;
    }
}
