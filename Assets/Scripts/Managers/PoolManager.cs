using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string key;
        public GameObject prefab;
        public int prewarm = 5;
    }

    public static PoolManager Instance;
    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> dict;
    private int NPCCounter;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        dict = new Dictionary<string, Queue<GameObject>>();

        foreach (var p in pools)
        {
            var q = new Queue<GameObject>(p.prewarm);
            for (int i = 0; i < p.prewarm; i++)
            {
                var go = Instantiate(p.prefab, transform);
                go.SetActive(false);
                q.Enqueue(go);
            }
            dict[p.key] = q;
        }

    }
    public GameObject Spawn(string key, Vector3 pos, Quaternion rot)
    {
        var q = dict[key];
        GameObject go = (q.Count > 0) ? q.Dequeue() : Instantiate(GetPrefab(key), transform);
        go.transform.SetPositionAndRotation(pos, rot);
        
        PeopleCounter.Register(key);
        go.SetActive(true);
        return go;
    }

    public void Despawn(string key, GameObject go)
    {
        go.SetActive(false);
        PeopleCounter.Unregister(key);
        dict[key].Enqueue(go);
    }

    GameObject GetPrefab(string key)
    {
        foreach (var p in pools) if (p.key == key) return p.prefab;
        Debug.LogError($"Pool key not found: {key}");
        return null;
    }

}
