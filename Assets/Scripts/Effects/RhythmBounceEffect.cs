using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class RhythmBounceEffect : MonoBehaviour
{
    [SerializeField] private float m_bounceFactor = 0.1f;
    [SerializeField] private float m_bounceDuration = 0.2f;
    void Start()
    {
        Sequencer.OnBar += Bounce;
    }

    private void Bounce()
    {
        transform.DOPunchScale(Vector2.one * m_bounceFactor, m_bounceDuration);
    }
}