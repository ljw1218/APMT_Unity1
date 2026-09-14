using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UIElements;

public class W_Pipe : Weapon
{
    [SerializeField] private TrailRenderer trail;

    [SerializeField] private float WaitTime = 0.3f;

    [SerializeField] private float PreAngle = 10f;
    [SerializeField] private float AfterAngle = -75f;

    [SerializeField] private Vector2 StartLocation = new Vector2(0.8f, 1.5f);
    [SerializeField] private Vector2 EndLocation = new Vector2(1.8f, -1.6f);

    [SerializeField] private float hitWidth = 0.9f;
    [SerializeField] private LayerMask layer;
    private string EnemyTag = "Enemy";
    private bool isAttack = false;

    protected override void Attack()
    {
        if (isAttack)
            return;
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        isAttack = true;

        Vector2 LockedStart = transform.parent.TransformPoint(StartLocation);
        Vector2 LockedEnd = transform.parent.TransformPoint(EndLocation);

        transform.localPosition = StartLocation;
        transform.localRotation = Quaternion.Euler(0f, 0f, PreAngle);
        trail.emitting = false;
        yield return new WaitForSeconds(WaitTime);

        trail.Clear();
        trail.emitting = true;
        CheckHit(LockedStart,LockedEnd);

        transform.position = LockedEnd;
        transform.rotation = Quaternion.Euler(0f, 0f, AfterAngle);

        yield return null;
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
        Vector3 LockedStart = transform.parent.TransformPoint(StartLocation);
        Vector3 LockedEnd = transform.parent.TransformPoint(EndLocation);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(LockedStart,LockedEnd);
        Gizmos.DrawWireSphere(LockedStart, hitWidth);
        Gizmos.DrawWireSphere(LockedEnd, hitWidth);
    }
}
