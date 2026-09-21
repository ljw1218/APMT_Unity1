using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UIElements;

public class W_Pipe : Weapon
{
    [SerializeField] private TrailRenderer trail;

    [SerializeField] private float WaitTime;

    [SerializeField] private float PreAngle = 10f;
    [SerializeField] private float AfterAngle = -75f;

    [SerializeField] private Vector2 StartLocation = new Vector2(0.8f, 1.5f);
    [SerializeField] private Vector2 EndLocation = new Vector2(1.8f, -1.6f);

    [SerializeField] private float hitWidth = 0.9f;
    [SerializeField] private LayerMask layer;

    private Vector2 LockedStart;
    private Vector2 LockedEnd;

    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
    }
    
    protected override IEnumerator AttackRoutine()
    {
        sr.enabled = true;
        bool isLeft = mousePos.x < BasePos.x;
        int flip = isLeft ? -1 : 1;

        float fromAngle = isLeft ? -1 * PreAngle : PreAngle;
        float toAngle = isLeft ? -1 * AfterAngle : AfterAngle;
        Vector2 from = isLeft ? new Vector2(-1*StartLocation.x,StartLocation.y) : StartLocation;
        Vector2 to = isLeft ? new Vector2(-1*EndLocation.x,EndLocation.y) : EndLocation;

        sr.flipX = !isLeft;

        //LockedStart = BasePos + from;
        //LockedEnd = BasePos + to;

        //transform.localPosition = LockedStart;
        //transform.localRotation = Quaternion.Euler(0f, 0f, fromAngle);
        transform.rotation = Quaternion.Euler(0f, 0f, fromAngle);

        //yield return new WaitForSeconds(WaitTime);
        float t = 0f;
        while (t < WaitTime)
        {
            transform.position = (Vector2)player.transform.position + from;
            t += Time.deltaTime;
            yield return null;
        }
        Vector2 origin = player.transform.position;
        LockedStart = origin + from;
        LockedEnd = origin + to;

        transform.position = LockedStart;

        trail.Clear();
        trail.emitting = true;
        CheckHit(LockedStart,LockedEnd);

        transform.position = LockedEnd;
        transform.rotation = Quaternion.Euler(0f, 0f, toAngle);

        yield return new WaitForSeconds(WaitTime);
        trail.emitting = false;
        sr.enabled = false;
        yield return false;
    }

    private void CheckHit(Vector2 StartPosition,Vector2 EndPosition)
    {
        Vector2 min = Vector2.Min(StartPosition,EndPosition) - Vector2.one * hitWidth;
        Vector2 max = Vector2.Max(StartPosition, EndPosition) + Vector2.one * hitWidth;

        Collider2D[] candidates = Physics2D.OverlapAreaAll(min, max, layer);

        foreach(var col in candidates)
        {
            Vector2 targetPos = col.transform.position;
            float distToLine = HitGeometry.DistancePointToSegment(targetPos, StartPosition, EndPosition);

            if(distToLine <= hitWidth)
            {
                ApplyDamage(col);
            }
        }
    }

    private void ApplyDamage(Collider2D col)
    {
        Enemy enemy = col.GetComponent<Enemy>();
        enemy.HitAction(Damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(LockedStart,LockedEnd);
        Gizmos.DrawWireSphere(LockedStart, hitWidth);
        Gizmos.DrawWireSphere(LockedEnd, hitWidth);
    }
}
