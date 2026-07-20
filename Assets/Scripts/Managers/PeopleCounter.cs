using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleCounter:MonoBehaviour
{
    public static int ActiveTotal { get; private set; }

    public static readonly Dictionary<string, int> ActiveByType=new Dictionary<string, int>();

    // Call when an NPC becomes active (spawned from pool)
    public static void Register(string key)
    {
        ActiveTotal++;
        if (!ActiveByType.ContainsKey(key)) ActiveByType[key] = 0;
        ActiveByType[key]++;
        //Debug.Log(ActiveByType[key]);
    }

    // Call when an NPC is deactivated (despawned to pool)
    public static void Unregister(string key)
    {
        ActiveTotal = Mathf.Max(0, ActiveTotal - 1);
        if (ActiveByType.ContainsKey(key))
            ActiveByType[key] = Mathf.Max(0, ActiveByType[key] - 1);
    }

    public static int GetActive(string key) =>
        ActiveByType.TryGetValue(key, out var v) ? v : 0;

}
