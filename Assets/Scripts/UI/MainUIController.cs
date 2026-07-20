using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public static MainUIController instance;
    public SanPanelControl sanPanel;
    public CountingDownUI countingDown;
    private void Awake()
    {
        if (instance != null) { Destroy(this); } else { instance = this; }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateCountingDownTime(int mm,int ss) {
        countingDown.UpdateCountDownTimer(mm,ss);
    }
   
}
