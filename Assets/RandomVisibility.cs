using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RandomVisibility : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_sprite;

    void Update()
    {
        //TODO: Every X time (random between two values) flip a coin (do a random with probability) and check if we should be visible or not
        //TODO: fade to new state if it changed
    }
}