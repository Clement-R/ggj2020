using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class ZoneRepairedEffect : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> m_ui;
    [SerializeField] private RectTransform m_text;

    [ContextMenu("Play")]
    public void Play()
    {
        float startX = m_text.anchoredPosition.x;
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(
                () =>
                {
                    m_ui.ForEach(s => s.DOFade(0f, 0.5f));
                }
            )
            .Join(m_text.DOAnchorPosX(0f, 0.75f).SetDelay(0.2f))
            .AppendInterval(2.5f)
            .AppendCallback(
                () =>
                {
                    m_ui.ForEach(s => s.DOFade(1f, 0.5f).SetDelay(0.2f));
                }
            )
            .Join(m_text.DOAnchorPosX(-startX, 0.75f))
            .OnComplete(
                () =>
                {
                    m_text.DOAnchorPosX(startX, 0f);
                }
            );
    }
}