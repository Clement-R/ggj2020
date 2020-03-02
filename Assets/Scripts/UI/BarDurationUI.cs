using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class BarDurationUI : MonoBehaviour
{
    [SerializeField] private Sequencer m_sequencer;
    [SerializeField] private string m_shaderProperty = "_CutOutAmount";
    [SerializeField] private float m_startValue = 1f;
    [SerializeField] private float m_endValue = 0f;
    private Material m_material;
    private Tweener m_tweener;

    private void Start()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer)
            m_material = renderer.material;

        var image = GetComponent<Image>();
        if (image)
            m_material = image.material;

        m_material.SetFloat(m_shaderProperty, m_startValue);

        m_sequencer.OnBar += Bar;
        m_sequencer.OnStop += Stop;
    }

    private void Stop()
    {
        m_tweener.Kill();
        m_material.SetFloat(m_shaderProperty, m_startValue);
    }

    private void Bar()
    {
        m_tweener?.Kill();

        m_material.SetFloat(m_shaderProperty, m_startValue);
        m_tweener = m_material.DOFloat(m_endValue, m_shaderProperty, m_sequencer.BarDuration).SetEase(Ease.Linear);
    }
}