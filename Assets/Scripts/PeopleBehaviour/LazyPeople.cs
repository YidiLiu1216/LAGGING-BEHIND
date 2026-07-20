using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyPeople : People
{
    // Start is called before the first frame update
    [Header("Lazy People Walking Parameter")]
    [SerializeField]private float maxLazySpeed = 0.05f;
    [SerializeField]private float minLazySpeed = 0.02f;
    [SerializeField] private float minStartLazyTime = 1.0f;
    [SerializeField] private float maxStartLazyTIme = 3.0f;
    private float startLazyTime;
    private float startLazyTimer = 0.0f;
    private bool isLazying=false;
    [Header("Lazy People Stop Parameter")]
    [SerializeField] private float stopProbability = 0.005f;
    private bool isStop=false;
    private bool isResume = false;
    
    void Start()
    {
        startLazyTime = Random.Range(minStartLazyTime, maxStartLazyTIme);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCarried) { return; }
        //initial status,coutting time for lazy
        if (isLazying == false && isResume == false)
        {

            startLazyTimer += Time.deltaTime;
            if (startLazyTimer >= startLazyTime)
            {
                isLazying = true;
            }
            base.Update();
        }//be slow and lazy
        else if (isStop == false && isResume == false)
        {
            float stop = Random.Range(0.0f, 1.0f);
            //sleep and stop
            if (stop < stopProbability) { isStop = true;animator.SetBool("isSleeping",true); }
            base.movePeople(minLazySpeed, maxLazySpeed);
        }
        else if (isResume == true) {
            base.Update();
        }
        
    }
    public void Resume() {
        isResume = true;
        isLazying = false;
        isStop = false;
        animator.SetBool("isSleeping", false);
    }
    public void BeginCarry() {
        //Debug.Log("new");
        if (isLazying) {
            Resume();
        }
        base.BeginCarry();
    }
   
}
