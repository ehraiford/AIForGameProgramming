using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float outerRadius;
    [Range(0, 360)]
    public float outerAngle;

    public float innerRadius;
    [Range(0, 360)]
    public float innerAngle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);


        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private bool innerCircle()
    {
        Collider[] innerRangeChecks = Physics.OverlapSphere(transform.position, innerRadius, targetMask);

        if (innerRangeChecks.Length != 0)
        {
            Transform target = innerRangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < innerAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else if (canSeePlayer)
        {
            return false;
        }
        return false;
    }
    private bool outerCircle()
    {
        Collider[] rangeChecker = Physics.OverlapSphere(transform.position, outerRadius, targetMask);

        if (rangeChecker.Length != 0)
        {
            Transform target = rangeChecker[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < outerAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else if (canSeePlayer)
        {
             return false;
        }
        return false;
    }
    private void FieldOfViewCheck()
    {
        if (innerCircle())
            canSeePlayer = true;
        else if (outerCircle())
            canSeePlayer = true;
        else
            canSeePlayer = false;
    }
}
