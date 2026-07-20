using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIAnimationCurve 
{
    
    public static float EaseInOutExpo(float t)
    {
        if (t <= 0f) return 0f;
        if (t >= 1f) return 1f;
        if (t < 0.5f)
            return Mathf.Pow(2f, 20f * t - 10f) / 2f;
        else
            return (2f - Mathf.Pow(2f, -20f * t + 10f)) / 2f;
    }
    public static float EaseInOutSine(float t)
    {
        return -(Mathf.Cos(Mathf.PI * t) - 1f) / 2f;
    }
}
