using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class PlanetZone
{
    public string Name;
    public EZone Zone;
    public GameObject[] LockedStates = new GameObject[3];
    public GameObject[] UnlockedStates = new GameObject[3];

    public int CurrentState = -1;
}