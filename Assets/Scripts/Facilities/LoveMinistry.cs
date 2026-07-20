using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveMinistry : Facilities
{
    private void Awake()
    {
        sanChangeValue = 1;
    }
    protected override void putNPCbyType(string tag)
    {
        AudioController.instance.playLoveMinistraySFX();
        AudioController.instance.playCharacterAMaleShock();
        PlayerData.instance.updateMinistryCount(tag);
    }
}
