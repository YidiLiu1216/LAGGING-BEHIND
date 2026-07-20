using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageDisposal : Facilities
{
   
    private void Awake()
    {
        sanChangeValue = 3;
    }
    protected override void changeSan(string tag) {
        if (tag != "Body") {
            PlayerData.instance.changeSan(-sanChangeValue);
        }
    }
    protected override void putNPCbyType(string tag)
    {
        if (tag != "Body") { AudioController.instance.playCharacterAMaleScream(); }
        AudioController.instance.playGarbageDisposalSFX();
        PlayerData.instance.updateGarbageDisposalCount(tag);
    }
}
