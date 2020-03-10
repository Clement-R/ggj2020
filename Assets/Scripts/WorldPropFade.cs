using UnityEngine;

using DG.Tweening;

public class WorldPropFade : WorldProp
{
    [SerializeField] private Vector2 m_fadeDurationRange;
    [SerializeField] private Vector2 m_delayBeforeFadeRange;

    private SpriteRenderer m_sr;
    private Tweener m_tweener;

    protected override void Start()
    {
        base.Start();

        m_sr = GetComponent<SpriteRenderer>();
    }

    public override void Show()
    {
        if (m_tweener != null)
        {
            m_tweener.Kill();
            m_sr.DOFade(0f, 0f);
        }

        m_tweener = m_sr
            .DOFade(1f, Random.Range(m_fadeDurationRange.x, m_fadeDurationRange.y))
            .SetDelay(Random.Range(m_delayBeforeFadeRange.x, m_delayBeforeFadeRange.y));
    }

    public override void Hide()
    {
        if (m_tweener != null)
        {
            m_tweener.Kill();
            m_sr.DOFade(1f, 0f);
        }

        m_tweener = m_sr
            .DOFade(0f, Random.Range(m_fadeDurationRange.x, m_fadeDurationRange.y))
            .SetDelay(Random.Range(m_delayBeforeFadeRange.x, m_delayBeforeFadeRange.y));
    }
}