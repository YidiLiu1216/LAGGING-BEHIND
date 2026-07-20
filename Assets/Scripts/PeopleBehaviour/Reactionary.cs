using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactionary : People
{
    // Start is called before the first frame update
    [Header("Reactionary People Stop Parameter")]
    [SerializeField] float maxStopTime = 3.0f;
    [SerializeField] float minStopTime = 1.0f;
    [SerializeField] float StopProbability = 0.01f;
    bool isStop;
    float stopTimer = 0.0f;

    [Header("Reactionary People Readin Paper")]
    [SerializeField] float maxReadingTime = 3.0f;
    [SerializeField] float minReadingTime = 1.0f;
    [SerializeField] float readProbability = 0.005f;
    bool isReading;
    float readingTimer = 0.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            float stopprob = Random.Range(0.0f, 1.0f);
            if (stopprob < StopProbability)
            {
                stopTimer = Random.Range(minStopTime, maxStopTime);
                isStop = true;
            }
            else {
                if (!isReading)
                {
                    float readingprob = Random.Range(0.0f, 1.0f);
                    if (readingprob < readProbability)
                    {
                        setReading();
                    }
                }
                else {
                    readingTimer -= Time.deltaTime;
                    if (readingTimer < 0.0f) {
                        closeReading();
                    }
                }
                base.Update();
            }
        }
        else {
            stopTimer -= Time.deltaTime;
            if (stopTimer < 0.0f) {
                Resume();
            }
        }
        
       
    }
    void setReading() {
        readingTimer = Random.Range(minReadingTime, maxReadingTime);
        animator.SetBool("isReading",true);
        isReading = true;

    }
    void closeReading() {
        animator.SetBool("isReading", false);
        isReading = false;
        readingTimer = 0.0f;
    }
    public void Resume() {
        isStop = false;
        stopTimer = 0.0f;
    }
}
