using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class PlanetZone
{
    public string Name;
    public EZone Zone;

    public Sample[] SamplesToUnlock = new Sample[3];

    public int CurrentState = -1;

    public bool Unlocked = false;
}