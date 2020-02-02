using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "Sample", menuName = "ScriptableObjects/Sample", order = 1)]
public class Sample : ScriptableObject
{
    public AudioClip Clip;
}