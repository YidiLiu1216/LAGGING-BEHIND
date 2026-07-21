using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleAvoidence : MonoBehaviour
{
    public float separationDistance = 0.5f;
    public float forceStrength = 1f;

    void FixedUpdate()
    {
        Collider2D[] neighbors = Physics2D.OverlapCircleAll(transform.position, separationDistance);
        Vector2 totalForce = Vector2.zero;

        foreach (var neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject)
            {
                Vector2 direction = (Vector2)transform.position - (Vector2)neighbor.transform.position;
                float distance = direction.magnitude;
                if (distance > 0)
                {
                    totalForce += direction.normalized / distance; 
                }
            }
        }

        transform.position += (Vector3)(totalForce * forceStrength * Time.fixedDeltaTime);
    }
    
}
