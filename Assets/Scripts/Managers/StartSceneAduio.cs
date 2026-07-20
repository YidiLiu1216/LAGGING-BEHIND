using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneAduio : MonoBehaviour
{
    public static StartSceneAduio instance;
    [SerializeField] AudioSource menuSwitchSFX;
    [SerializeField] AudioSource menuSelectSFX;

    private void Awake()
    {
        if (instance == null) { instance = this; } else { Destroy(this); }
    }
    public void PlayMenuSwithSFX() {
        menuSwitchSFX.Play();
    }
    public void PlayMenuSelectSFX() {
        menuSelectSFX.Play();
    }
}
