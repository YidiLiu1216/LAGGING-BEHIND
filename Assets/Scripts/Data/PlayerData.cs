using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    //public int shootPeopleCountToday { get; private set; }
    //public int loveMinistryCountToday { get; private set; }
    //public int garbageDisposalCountToday { get; private set; }
    public List<string> PeopleType = new List<string>();
    public readonly Dictionary<string, int> KillsByType = new Dictionary<string, int>();
    public readonly Dictionary<string, int> LoveMinistryByType = new Dictionary<string, int>();
    public readonly Dictionary<string, int> GarbageDisposalByType = new Dictionary<string, int>();


    public int maxPlayerSan { get; private set; }
    private int playerSan;

    public static PlayerData instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else {
            instance = this;
        }
        maxPlayerSan = 100;
        playerSan = maxPlayerSan;
        initPlayerData();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeSan(int value) {
        playerSan = Mathf.Min(playerSan + value, playerSan);
        playerSan = Mathf.Max(playerSan, 0);
        //Debug.Log("player san : " + playerSan);
        SanPanelControl.instance.SetValue(playerSan);
    }
    public void updateKillCount(string tag) {
        KillsByType[tag] += 1;
        //Debug.Log(tag+" count: "+ KillsByType[tag]);
    }
    public void updateMinistryCount(string tag) {
        LoveMinistryByType[tag] += 1;
    }
    public void updateGarbageDisposalCount(string tag) {
        GarbageDisposalByType[tag] += 1;
    }
    void initPlayerData() {
        foreach (string type in PeopleType) {
            KillsByType[type] = 0;
            LoveMinistryByType[type] = 0;
            GarbageDisposalByType[type] = 0;
        }
  
    }
}
