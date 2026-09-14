using UnityEngine;

public static class HitGeometry
{
    //점에서 선분까지 최단거리
    public static float DistancePointToSegment(Vector2 point, Vector2 a, Vector2 b)
    {
        Vector2 ab = b - a;
        float sqrLen = ab.sqrMagnitude; // 비용절감
        if (sqrLen < 0.0001f)
            return Vector2.Distance(point, a);

        float t = Mathf.Clamp01(Vector2.Dot(point - a, ab) / sqrLen);
        //0아래면 a 바깥쪽, 1이상은 b바깥쪽. 캡슐모양으로 만들기 위함
        Vector2 projection = a + t * ab;
        //point에서 선분으로 내린 수선의 발
        return Vector2.Distance(point, projection);
        //실제 거리 반환
    }

    //시작은 넓지만 점점 좁아지는 영역 안에 있는지 판단
    public static bool IsInTaperedArea(Vector2 point, Vector2 a, Vector2 b, float startWidth, float endWidth)
    {
        Vector2 ab = b - a;
        float sqrLen = ab.sqrMagnitude;
        if (sqrLen < 0.0001f)
            return Vector2.Distance(point, a) <= startWidth;

        float t = Vector2.Dot(point - a, ab) / sqrLen;
        //수선의 발이 몇퍼센트 지점인지
        if (t < 0f || t > 1f)
            return false;

        Vector2 projection = a + t * ab;
        //point to ab 수선의 발
        float allowedWidth = Mathf.Lerp(startWidth, endWidth, t);
        //몇퍼센트 지점인지
        return Vector2.Distance(point, projection) <= allowedWidth;
    }

    public static bool IsInSector(Vector2 point, Vector2 center, Vector2 forward, float radius,float angle)
    {
        Vector2 toTarget = point - center;
        if (toTarget.sqrMagnitude > Mathf.Pow(radius, 2))
            return false;

        return Vector2.Angle(forward, toTarget) <= angle * 0.5f;
        //중심선 기준으로 좌우이므로 0.5
    }
}
