using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using DG.Tweening;

public class SeedsPackageUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Sample Sample => m_sample;
    public Sprite Flower => m_flower.sprite;
    public Color Color => m_color;

    [SerializeField] private Image m_flower;
    [SerializeField] private Image m_top;
    [SerializeField] private Image m_background;
    [SerializeField] private Sample m_sample;
    [SerializeField] private Color m_color;

    [SerializeField] private ScrollRect m_scrollRect;
    [SerializeField] private Camera m_camera;
    [SerializeField] private float m_dragThreshold = 0.08f;

    [SerializeField] private float m_dragEndDownFactor = 1f;

    [SerializeField] private GameObject m_emptyPackagePrefab;
    [SerializeField] private RectTransform m_container;
    [SerializeField] private CanvasGroup m_containerCanvasGroup;

    private GameObject m_emptyPackage;

    private RectTransform m_rectTransform;

    private bool m_isScrolling = false;
    private bool m_isDragging = false;

    private Vector3 m_delta;
    private Transform m_parent;
    private int m_siblingIndex;

    void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_parent = m_rectTransform.parent;
        m_siblingIndex = transform.GetSiblingIndex();
    }

    private void OnValidate()
    {
        m_background.color = m_color.SetA(1f);
        m_top.color = m_color.DarkerShade();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (m_scrollRect != null && eventData.delta.y < m_dragThreshold && eventData.delta.y > -m_dragThreshold)
        {
            m_isScrolling = true;
            m_scrollRect.SendMessage("OnBeginDrag", eventData);
            return;
        }

        // Create empty slot at position in scroll
        m_emptyPackage = Instantiate(m_emptyPackagePrefab, m_parent);
        m_emptyPackage.transform.SetSiblingIndex(m_siblingIndex);

        transform.parent = m_scrollRect.transform.parent;

        m_isDragging = true;
        m_delta = m_camera.ScreenToWorldPoint(eventData.position) - m_rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_isScrolling)
        {
            m_scrollRect.OnDrag(eventData);
        }

        if (m_isDragging)
        {
            Vector3 pos = m_camera.ScreenToWorldPoint(eventData.position) + m_delta;
            pos.z = 0f;
            m_rectTransform.position = pos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_isDragging)
        {
            //TODO: Remove empty slot in scroll
            Destroy(m_emptyPackage);

            transform.parent = m_parent;
            transform.SetSiblingIndex(m_siblingIndex);

            // Tween back to original position
            StartCoroutine(_EndDragEffect());

            m_isDragging = false;
        }

        if (m_isScrolling)
        {
            m_scrollRect.OnEndDrag(eventData);
            m_isScrolling = false;
        }
    }

    private IEnumerator _EndDragEffect()
    {
        m_containerCanvasGroup.DOFade(0f, 0f);

        yield return null;

        m_containerCanvasGroup.DOFade(1f, 0.35f);

        Vector3 startPosition = transform.position;

        m_container.position = startPosition - Vector3.up * m_dragEndDownFactor;
        m_container.DOMoveY(startPosition.y, 0.5f).SetDelay(0.35f);
    }
}