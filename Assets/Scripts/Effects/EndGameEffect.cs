using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using TMPro;

using DG.Tweening;

public class EndGameEffect : MonoBehaviour
{
    [SerializeField] private List<FlowerPot> m_flowerPots;
    [SerializeField] private CanvasGroup m_gameUI;
    [SerializeField] private RectTransform m_gameCompleteTransform;
    [SerializeField] private TextMeshProUGUI m_gameCompleteSubtitle;

    [Header("Settings")]
    [SerializeField] private float m_gameUiFadeOutDuration;
    [SerializeField] private float m_delayBeforeSubtitle;
    [SerializeField] private float m_subtitleFadeInDuration;

    private List<SpriteRenderer> m_gameRenderers = new List<SpriteRenderer>();

    private void Start()
    {
        m_flowerPots.ForEach(f => m_gameRenderers.AddRange(f.GetComponentsInChildren<SpriteRenderer>(true)));
    }

    [ContextMenu("Play effect")]
    public void PlayEffect()
    {
        float startX = m_gameCompleteTransform.anchoredPosition.x;

        m_gameCompleteSubtitle.DOFade(0f, 0f);

        List<SpriteRenderer> activeSprites = m_gameRenderers.Where(r => r.color.a > 0.1f).ToList();

        Sequence seq = DOTween.Sequence();
        seq
            .AppendCallback(
                () =>
                {
                    activeSprites.ForEach(r => r.DOFade(0f, m_gameUiFadeOutDuration));
                }
            )
            .Append(
                m_gameUI.DOFade(0f, m_gameUiFadeOutDuration)
            )
            .Join(
                // Slide in
                m_gameCompleteTransform.DOAnchorPosX(0f, 0.75f).SetDelay(m_gameUiFadeOutDuration - (m_gameUiFadeOutDuration * 0.5f))
            )
            .AppendInterval(m_delayBeforeSubtitle)
            .Append(
                // Fade in subtitle
                m_gameCompleteSubtitle.DOFade(1f, m_subtitleFadeInDuration)
            )
            .AppendInterval(3f)
            .AppendCallback(
                () =>
                {
                    activeSprites.ForEach(r => r.DOFade(1f, m_gameUiFadeOutDuration));
                }
            )
            .Append(
                m_gameUI.DOFade(1f, m_gameUiFadeOutDuration)
            )
            .Join(
                m_gameCompleteTransform.DOAnchorPosX(-startX, 0.75f)
            )
            .OnComplete(
                () =>
                {
                    m_gameCompleteTransform.DOAnchorPosX(startX, 0f);
                }
            );;

    }
}