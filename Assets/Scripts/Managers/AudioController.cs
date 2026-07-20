using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    [SerializeField] AudioSource backgroundMusic;
    [Header("Environment SFX")]
    [SerializeField] AudioSource loveMinistraySFX;
    [SerializeField] AudioSource garbageDisposalSFX;
    [SerializeField] AudioSource shootSFX;
    [SerializeField] AudioSource grabSFX;
    [Header("UI SFX")]
    [SerializeField] AudioSource menuSwithcSFX;
    [SerializeField] AudioSource menuSelectSFX;
    [Header("Character SFX")]
    [SerializeField] List<AudioSource> characterMaleShock;
    [SerializeField] List<AudioSource> characterMaleScream;


    private void Awake()
    {
        if (instance == null) { instance = this; } else { Destroy(this); }
        
    }
    public void playLoveMinistraySFX() {
        loveMinistraySFX.Play();
    }
    public void playGarbageDisposalSFX() {
        garbageDisposalSFX.Play();
    }
    public void playShootSFX() {
        shootSFX.Play();
    }
    public void playMenuSwitchSFX() {
        menuSwithcSFX.Play();
    }
    public void playMenuSelectSFX() {
        menuSelectSFX.Play();
    }
    public void playGrabSFX() {
        grabSFX.Play();
    }
    public void playCharacterAMaleShock() {
        int i = Random.Range(0, characterMaleShock.Count);
        characterMaleShock[i].Play();
    }
    public void playCharacterAMaleScream()
    {
        int i = Random.Range(0, characterMaleScream.Count);
        characterMaleScream[i].Play();
    }
    public void playBackgroundMusic() {
        backgroundMusic.Play();
    }
    public void pauseBackgroundMusic() {
        backgroundMusic.Pause();
    }
    public void unpauseBackgroundMusic() {
        backgroundMusic.UnPause();
    }
    public void StopBackgroundMusic() {
        backgroundMusic.Stop();
    }
}
