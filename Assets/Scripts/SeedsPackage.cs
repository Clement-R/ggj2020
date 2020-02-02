using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Lean.Touch;

public class SeedsPackage : MonoBehaviour
{
    public Sample Sample => m_sample;

    [SerializeField] private Sample m_sample;
    [SerializeField] private BoxCollider2D m_collider;
    private LeanFinger m_finger;

    private Camera m_camera;
    private Vector3 m_startPosition;
    private Vector3 m_delta;

    private void Start()
    {
        m_camera = Camera.main;
        m_startPosition = transform.position;

        LeanTouch.OnFingerUp += FingerUp;
        LeanTouch.OnFingerDown += FingerDown;
    }

    private void Update()
    {
        if (m_finger != null)
        {
            transform.position = (Vector2) (m_camera.ScreenToWorldPoint(m_finger.ScreenPosition) - m_delta);
        }
    }

    private void FingerDown(LeanFinger p_finger)
    {
        if (m_finger != null)
            return;

        if (PositionIsOver(p_finger.ScreenPosition))
        {
            m_delta = m_camera.ScreenToWorldPoint(p_finger.ScreenPosition) - transform.position;
            m_finger = p_finger;
        }
    }

    private void FingerUp(LeanFinger p_finger)
    {
        if (m_finger == null)
            return;

        m_finger = null;
        transform.position = m_startPosition;
    }

    private bool PositionIsOver(Vector3 p_screenPosition)
    {
        var worldPosition = m_camera.ScreenToWorldPoint(p_screenPosition);
        return m_collider.OverlapPoint(worldPosition);
    }
}