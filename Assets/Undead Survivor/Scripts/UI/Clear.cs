using UnityEngine;

public class Clear : MonoBehaviour
{
    RectTransform rect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
    }

    public void Show()
    {
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }
    
}
