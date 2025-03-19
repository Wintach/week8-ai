using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    [Header("FOV Settings")]
    public float viewRadius = 10f;
    [Range(0, 360)]
    public float viewAngle = 90f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    Collider[] targetsInViewRadius;

    EnemyMover _EMover;
    

    bool chaseMode;

    void Start()
    {
        _EMover = GetComponent<EnemyMover>();
    }

    void Update()
    {
        targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        foreach (Collider target in targetsInViewRadius)
        {
            if(target.tag == "hero")
            {
                Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                    {
                        _EMover.setTarget(target.transform.position);
                        break;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Set Gizmo color
        Gizmos.color = Color.yellow;

        // Draw FOV Lines
        Vector3 leftBoundary = DirectionFromAngle(-viewAngle / 2);
        Vector3 rightBoundary = DirectionFromAngle(viewAngle / 2);

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewRadius);

        // Draw detection radius
        Gizmos.color = new Color(1, 1, 0, 0.2f); // Yellow transparent
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        // Draw rays to detected targets
        Gizmos.color = Color.red;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        foreach (Collider target in targetsInViewRadius)
        {
            Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                {
                    Gizmos.DrawLine(transform.position, target.transform.position);
                }
            }
        }
    }

    // Get direction vector from angle
    private Vector3 DirectionFromAngle(float angle)
    {
        float radian = (transform.eulerAngles.y + angle) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
