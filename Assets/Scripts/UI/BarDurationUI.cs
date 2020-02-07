using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class BarDurationUI : MonoBehaviour
{
    [SerializeField] private Sequencer m_sequencer;
    private Material m_spriteDissolver;
    private Tweener m_tweener;

    private void Start()
    {
        m_spriteDissolver = GetComponent<SpriteRenderer>().material;

        m_sequencer.OnBar += Bar;
        m_sequencer.OnStop += Stop;
    }

    private void Stop()
    {
        m_tweener.Kill();
        m_spriteDissolver.SetFloat("_CutOutAmount", 1f);
    }

    private void Bar()
    {
        m_tweener?.Kill();

        m_spriteDissolver.SetFloat("_CutOutAmount", 1f);
        m_tweener = m_spriteDissolver.DOFloat(0f, "_CutOutAmount", m_sequencer.BarDuration).SetEase(Ease.Linear);
    }
}