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

    [SerializeField] private Image m_flower;
    [SerializeField] private Image m_top;
    [SerializeField] private Image m_background;
    [SerializeField] private Sample m_sample;
    [SerializeField] private Color m_color;

    [SerializeField] private ScrollRect m_scrollRect;
    [SerializeField] private Camera m_camera;
    [SerializeField] private float m_dragThreshold = 0.08f;

    private RectTransform m_rectTransform;

    private bool m_isScrolling = false;
    private bool m_isDragging = false;

    private Vector3 m_delta;
    private Vector3 m_startPosition;
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
        m_background.color = m_color;

        float h;
        float s;
        float v;
        Color.RGBToHSV(m_color, out h, out s, out v);
        s += 0.2f;
        m_top.color = Color.HSVToRGB(h, s, v);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (m_scrollRect != null && eventData.delta.y < m_dragThreshold && eventData.delta.y > -m_dragThreshold)
        {
            m_isScrolling = true;
            m_scrollRect.SendMessage("OnBeginDrag", eventData);
            return;
        }

        transform.parent = m_scrollRect.transform.parent;

        m_startPosition = m_rectTransform.position;
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
            transform.parent = m_parent;
            transform.SetSiblingIndex(m_siblingIndex);

            // transform.position = m_startPosition - Vector3.up * 65f;
            // transform.DOMoveY(m_startPosition.y, 0.5f).SetDelay(0.15f);

            m_isDragging = false;
        }

        if (m_isScrolling)
        {
            m_scrollRect.OnEndDrag(eventData);
            m_isScrolling = false;
        }
    }
}