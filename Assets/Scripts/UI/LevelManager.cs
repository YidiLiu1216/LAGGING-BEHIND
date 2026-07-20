using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Count Down Timer Setting")]
    public int LevelSeconds = 90;
    [SerializeField] private bool useUnscaledTime = false;
    [SerializeField] private float scorePanelPopUpTime = 1.0f;
    public bool isLevelPausing {get;set;}
    float timeLeft;
    bool isOver;
    private void Awake()
    {
        isLevelPausing = false;
    }
    void Start()
    {
        timeLeft = Mathf.Max(0f, LevelSeconds);

    }

    // Update is called once per frame
    void Update()
    {
        if (isOver) return;

        float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        timeLeft -= dt;
        calculateTimetoMiniutesandSeconds();
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;

            triggerLevelOver();
            isOver = true;
            return;
        }

       
    }
    void triggerLevelOver() {
        
        SanPanelControl.instance.ShowPanel();
        AudioController.instance.StopBackgroundMusic();
        Time.timeScale = 0.0f;

    }
    void calculateTimetoMiniutesandSeconds()
    {
        
        int total = Mathf.CeilToInt(timeLeft);
        if (total < 0) total = 0;
        int mm = total / 60;
        int ss = total % 60;
        MainUIController.instance.updateCountingDownTime(mm, ss);
    }
    

}
