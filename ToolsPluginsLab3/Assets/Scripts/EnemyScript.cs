using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform target;
    private float speed = 20f;
    private float rotationModifier = 90f;
    private float rotationSpeed = 30f;
    private float initialDistance;
    private float adjustedSpeed;

    private void Start()
    {
        if (target != null)
        {
            // Store the initial distance between the enemy and the player
            initialDistance = Vector3.Distance(transform.position, target.position);

            // Calculate adjusted rotation speed based on initial distance
            adjustedSpeed = rotationSpeed * initialDistance;
        }
    }

    void Update()
    {
        LookAtPlayer();
        MaintainDistance();
        RotateAroundPlayer();
    }

    private void LookAtPlayer()
    {
        if (target != null)
        {
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, quat, Time.deltaTime * speed);
        }
    }

    private void RotateAroundPlayer()
    {
        if (target != null)
        {
            transform.RotateAround(target.position, Vector3.forward, adjustedSpeed * Time.deltaTime);
        }
    }

    private void MaintainDistance()
    {
        if (target != null)
        {
            Vector3 directionToTarget = (transform.position - target.position).normalized;
            transform.position = target.position + directionToTarget * initialDistance;
        }
    }
}
