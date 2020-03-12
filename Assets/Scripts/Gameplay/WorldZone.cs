using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class WorldZone : WorldProp
{
    [SerializeField] private float m_fadeDuration = 1f;
    [SerializeField] private float m_punchScaleFactor = 1.25f;
    [SerializeField] private float m_punchScaleDuration = 1f;

    private SpriteRenderer m_sr;
    private Tweener m_tweener;

    protected override void Start()
    {
        base.Start();
        m_sr = GetComponent<SpriteRenderer>();

        gameObject.SetActive(false);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
        transform
            .DOPunchScale(Vector2.one * m_punchScaleFactor, m_punchScaleDuration);
    }

    public override void Hide()
    {
        transform
            .DOPunchScale(Vector2.one * m_punchScaleFactor, m_punchScaleDuration)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}