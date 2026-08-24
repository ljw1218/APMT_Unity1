using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        //월드좌표와 스크린좌표가 다르기 때문
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
