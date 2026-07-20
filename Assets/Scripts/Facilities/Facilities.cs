using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facilities : MonoBehaviour
{
    public int sanChangeValue;
    public void putNPCinFacilities(string tag) {
        putNPCbyType(tag);
        changeSan(tag);
    }

    protected virtual void changeSan(string tag) {
        
        PlayerData.instance.changeSan(-sanChangeValue);
    }
    protected virtual void putNPCbyType(string tag) {
        
    }
}
