using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class RandomRhythmBounceEffect : MonoBehaviour
{
    [SerializeField] private Vector2 m_bounceDurationRange;
    [SerializeField] private Vector2 m_bounceFactorRange;
    [SerializeField, Range(0f, 1f)] private float m_probability;

    private float m_bounceDuration = 0.2f;
    private float m_bounceFactor = 0.1f;

    void Start()
    {
        Sequencer.OnBar += Bounce;
    }

    private bool ShouldBounce()
    {
        float rand = Random.Range(0f, 1f);
        return rand <= m_probability;
    }

    private void Bounce()
    {
        if (ShouldBounce())
        {
            m_bounceFactor = Random.Range(m_bounceFactorRange.x, m_bounceFactorRange.y);
            m_bounceDuration = Random.Range(m_bounceDurationRange.x, m_bounceDurationRange.y);

            transform.DOPunchScale(Vector2.one * m_bounceFactor, m_bounceDuration);
        }
    }
}