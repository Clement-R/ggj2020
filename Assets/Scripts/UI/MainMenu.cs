using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float m_startEffectDuration = 2.5f;
    [Header("Buttons")]
    [SerializeField] private Button m_start;
    [SerializeField] private Button m_twitterLink;
    [SerializeField] private Button m_kenneyLink;
    [SerializeField] private string m_twitterURL;
    [SerializeField] private string m_kenneyURL;

    [Header("Texts")]
    [SerializeField] private CanvasGroup m_name;
    [SerializeField] private CanvasGroup m_subName;
    [SerializeField] private CanvasGroup m_bottomInfos;

    [Header("Game UI")]
    [SerializeField] private List<SpriteRenderer> m_flowerPots;
    [SerializeField] private List<CanvasGroup> m_seedsPackages;

    [Header("Sounds")]
    [SerializeField] private AudioSource m_loopSource;
    [SerializeField] private AudioClip m_vinylNoise;

    private void Start()
    {
        SetupAudio();

        m_start.onClick.AddListener(Play);
        m_twitterLink.onClick.AddListener(TwitterLink);
        m_kenneyLink.onClick.AddListener(KenneyLink);
    }

    private void SetupAudio()
    {
        m_loopSource.loop = true;

        m_loopSource.clip = m_vinylNoise;

        m_loopSource.Play();
    }

    private void TwitterLink()
    {
        Application.OpenURL(m_twitterURL);
    }

    private void KenneyLink()
    {
        Application.OpenURL(m_kenneyURL);
    }

    private void Play()
    {
        m_loopSource.DOFade(0f, 3f);

        // Texts
        m_name.transform.DOMoveY(m_name.gameObject.transform.position.y + 1f, m_startEffectDuration);
        m_subName.transform.DOMoveY(m_subName.gameObject.transform.position.y + 1f, m_startEffectDuration);

        m_bottomInfos.transform.DOMoveY(m_bottomInfos.gameObject.transform.position.y - 0.5f, m_startEffectDuration);

        m_name.DOFade(0f, m_startEffectDuration);

        float textDuration = m_startEffectDuration - (m_startEffectDuration * 0.5f);
        m_subName.DOFade(0f, textDuration);
        m_bottomInfos.DOFade(0f, textDuration);

        // Buttons
        // m_start.transform.DOMoveY(m_start.gameObject.transform.position.y - 5f, 2.5f);
        m_twitterLink.transform.DOMoveY(m_twitterLink.gameObject.transform.position.y - 5f, m_startEffectDuration);
        m_kenneyLink.transform.DOMoveY(m_kenneyLink.gameObject.transform.position.y - 5f, m_startEffectDuration);

        m_start.GetComponent<CanvasGroup>().DOFade(0f, m_startEffectDuration - (m_startEffectDuration * 0.33f));
        m_twitterLink.GetComponent<CanvasGroup>().DOFade(0f, m_startEffectDuration);
        m_kenneyLink.GetComponent<CanvasGroup>().DOFade(0f, m_startEffectDuration);

        // Game UI
        float uiDuration = m_startEffectDuration + m_startEffectDuration * 0.5f;
        m_flowerPots.ForEach(s => s.DOFade(1f, uiDuration));
        m_seedsPackages.ForEach(s => s.DOFade(1f, uiDuration));
    }
}