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

    [SerializeField] private Sequencer m_sequencer;
    [SerializeField] private SpriteRenderer m_barDuration;
    [SerializeField] private List<GameObject> m_outlineParts;
    [SerializeField] private SpriteRenderer m_flowerRenderer;
    [SerializeField] private SpriteRenderer m_sampleBadge;
    [SerializeField] private Collider2D m_removeSampleButton;

    private bool m_outlined = false;
    private SeedsPackageUI m_hoveringPackage = null;
    private Material m_spriteDissolver;
    private Tweener m_appearEffect;
    private float m_appearEffectDuration = 1f;
    private Material m_barDurationMaterial;

    void Start()
    {
        m_spriteDissolver = m_flowerRenderer.material;
        m_barDurationMaterial = m_barDuration.material;

        m_barDuration.gameObject.SetActive(false);

        m_removeSampleButton.gameObject.SetActive(false);
        m_sampleBadge.gameObject.SetActive(false);

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

            if (m_sequencer.PlayingSamples.Count > 1)
            {
                m_barDuration.gameObject.SetActive(true);
                m_barDurationMaterial.SetFloat("_Amount", m_sequencer.BarProgression);
                m_barDurationMaterial.DOFloat(1f, "_Amount", m_sequencer.BarDuration - m_sequencer.BarPosition).SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        m_barDuration.gameObject.SetActive(false);
                    });
            }

            UpdateFlower(m_hoveringPackage);
        }
    }

    private void UpdateFlower(SeedsPackageUI p_package)
    {
        m_removeSampleButton.gameObject.SetActive(true);

        m_spriteDissolver.SetFloat("_CutOutAmount", 1f);
        m_flowerRenderer.sprite = p_package.Flower;

        m_appearEffect = m_spriteDissolver.DOFloat(0f, "_CutOutAmount", m_appearEffectDuration);

        m_sampleBadge.color = p_package.Color.DarkerShade().SetA(1f);
        m_sampleBadge.gameObject.SetActive(true);
    }

    private void RemoveFlower()
    {
        m_appearEffect?.Kill();

        var progression = m_spriteDissolver.GetFloat("_CutOutAmount");
        m_appearEffect = m_spriteDissolver.DOFloat(1f, "_CutOutAmount", (1f - progression) * m_appearEffectDuration).SetEase(Ease.Linear);

        m_sampleBadge.gameObject.SetActive(false);
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
            m_hoveringPackage = p_collider.gameObject.GetComponent<SeedsPackageUI>();

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