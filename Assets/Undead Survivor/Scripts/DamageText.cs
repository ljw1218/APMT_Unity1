using System.Threading;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 1.5f;
    [SerializeField] private float lifeTime = 0.7f;
    private TextMeshPro text;
    private float timer;

    private Color eDamageColor = new Color32(255, 156, 18,255);
    private Color curColor;
    
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    public void Init(int damage,bool isPlayer)
    {
        text.text = damage.ToString();
        if (isPlayer)
            text.color = Color.red;
        else
            text.color = eDamageColor;
        curColor = text.color;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;

        float alpha = 1f - timer / lifeTime;
        text.color = new Color(curColor.r, curColor.g, curColor.b, alpha);

        if (timer >= lifeTime)
            gameObject.SetActive(false);
    }
}
