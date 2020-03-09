using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private Transform m_around;
    [SerializeField] private float m_speed = 5f;

    void Update()
    {
        transform.RotateAround(m_around.position, Vector3.forward, m_speed);
    }
}