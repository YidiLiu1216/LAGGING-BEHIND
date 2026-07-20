using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string type = collision.tag;
        PoolManager.Instance.Despawn(type, collision.gameObject);
    }
}
