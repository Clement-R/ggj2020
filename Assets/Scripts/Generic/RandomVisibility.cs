using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class RandomVisibility : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_sprite;

    [SerializeField] private Vector2 m_timeBetweenCycle;
    [SerializeField] private Vector2 m_fadeDurationRange;
    [SerializeField, Range(0f, 1f)] private float m_probability;

    private bool m_isVisible = false;

    private void Awake()
    {
        m_sprite.DOFade(0f, 0f);
    }

    private void Start()
    {
        StartCoroutine(_Effect());
        RandomStateChange();
    }

    private IEnumerator _Effect()
    {
        while (true)
        {
            // Wait for next effect
            float randomDuration = Random.Range(m_timeBetweenCycle.x, m_timeBetweenCycle.y);
            for (float timer = 0f, duration = randomDuration; timer < duration; timer += Time.deltaTime)
            {
                yield return null;
            }

            RandomStateChange();
        }
    }

    private void RandomStateChange()
    {
        float rand = Random.Range(0f, 1f);
        if (rand <= m_probability)
        {
            if (m_isVisible == false)
            {
                m_isVisible = true;
                FadeIn();
            }
        }
        else
        {
            if (m_isVisible)
            {
                m_isVisible = false;
                FadeOut();
            }
        }
    }

    private void FadeIn()
    {
        m_sprite.DOFade(1f, Random.Range(m_fadeDurationRange.x, m_fadeDurationRange.y));
    }

    private void FadeOut()
    {
        m_sprite.DOFade(0f, Random.Range(m_fadeDurationRange.x, m_fadeDurationRange.y));
    }
}