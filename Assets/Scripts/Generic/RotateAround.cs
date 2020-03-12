using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private Transform m_around;
    [SerializeField] private float m_speed = 5f;

    [SerializeField] private bool m_randomSpeed;
    [SerializeField] private Vector2 m_speedRange;

    private void Start()
    {
        if (m_randomSpeed)
        {
            m_speed = Random.Range(m_speedRange.x, m_speedRange.y);
        }
    }

    void Update()
    {
        transform.RotateAround(m_around.position, Vector3.forward, m_speed);
    }
}