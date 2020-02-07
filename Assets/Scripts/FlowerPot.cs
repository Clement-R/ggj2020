using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Lean.Touch;

using DG.Tweening;

public class FlowerPot : MonoBehaviour
{
    public Action<Sample> OnSampleChange;
    public Action OnSampleRemoved;

    [SerializeField] List<GameObject> m_outlineParts;
    [SerializeField] SpriteRenderer m_flowerRenderer;
    [SerializeField] Collider2D m_removeSampleButton;

    private bool m_outlined = false;
    private SeedsPackage m_hoveringPackage = null;
    private Material m_spriteDissolver;
    private Tweener m_appearEffect;
    private float m_appearEffectDuration = 1f;

    void Start()
    {
        m_spriteDissolver = m_flowerRenderer.material;

        m_removeSampleButton.gameObject.SetActive(false);

        LeanTouch.OnFingerTap += FingerTap;
        LeanTouch.OnFingerUp += FingerUp;
    }

    private void FingerTap(LeanFinger p_finger)
    {
        if (Helpers.PositionIsOver(Camera.main, m_removeSampleButton, p_finger.ScreenPosition))
        {
            RemoveSample();
        }
    }

    private void FingerUp(LeanFinger p_finger)
    {
        if (m_hoveringPackage != null)
        {
            OnSampleChange?.Invoke(m_hoveringPackage.Sample);
            UpdateFlower(m_hoveringPackage.Flower);
        }
    }

    private void UpdateFlower(Sprite p_flower)
    {
        m_removeSampleButton.gameObject.SetActive(true);

        m_spriteDissolver.SetFloat("_CutOutAmount", 1f);
        m_flowerRenderer.sprite = p_flower;

        m_appearEffect = m_spriteDissolver.DOFloat(0f, "_CutOutAmount", m_appearEffectDuration);
    }

    private void RemoveFlower()
    {
        m_appearEffect?.Kill();

        var progression = m_spriteDissolver.GetFloat("_CutOutAmount");
        m_appearEffect = m_spriteDissolver.DOFloat(1f, "_CutOutAmount", (1f - progression) * m_appearEffectDuration).SetEase(Ease.Linear);
    }

    private void RemoveSample()
    {
        m_removeSampleButton.gameObject.SetActive(false);

        OnSampleRemoved?.Invoke();
        RemoveFlower();
    }

    private void UpdateOutline()
    {
        m_outlineParts.ForEach(o => o.SetActive(m_outlined));
    }

    private void OnTriggerEnter2D(Collider2D p_collider)
    {
        if (p_collider.gameObject.CompareTag("SeedsPackage"))
        {
            m_hoveringPackage = p_collider.gameObject.GetComponent<SeedsPackage>();

            m_outlined = true;
            UpdateOutline();
        }
    }

    private void OnTriggerExit2D(Collider2D p_collider)
    {
        if (p_collider.gameObject.CompareTag("SeedsPackage") && m_outlined)
        {
            m_hoveringPackage = null;

            m_outlined = false;
            UpdateOutline();
        }
    }
}