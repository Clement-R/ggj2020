using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float m_bounceFactor = 0.1f;
    [SerializeField] private float m_bounceDuration = 0.2f;
    [SerializeField] private int m_vibrato = 2;
    [SerializeField] private float m_elasticity = 0.5f;

    private void Start()
    {
        transform.DOPunchScale(Vector2.one * m_bounceFactor, m_bounceDuration, m_vibrato, m_elasticity).SetDelay(0.25f).SetLoops(-1);
    }
}