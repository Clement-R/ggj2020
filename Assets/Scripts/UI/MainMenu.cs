using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button m_start;
    [SerializeField] private Button m_twitterLink;
    [SerializeField] private Button m_kenneyLink;
    [SerializeField] private string m_twitterURL;
    [SerializeField] private string m_kenneyURL;

    [Header("Texts")]
    [SerializeField] private CanvasGroup m_name;
    [SerializeField] private CanvasGroup m_subName;

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
        m_name.transform.DOMoveY(m_name.gameObject.transform.position.y + 1f, 2.5f);
        m_subName.transform.DOMoveY(m_subName.gameObject.transform.position.y + 1f, 2.5f);

        m_name.DOFade(0f, 2.5f);
        m_subName.DOFade(0f, 2f);

        // Buttons
        m_start.transform.DOMoveY(m_start.gameObject.transform.position.y - 5f, 2.5f);
        m_twitterLink.transform.DOMoveY(m_twitterLink.gameObject.transform.position.y - 5f, 2.5f);
        m_kenneyLink.transform.DOMoveY(m_kenneyLink.gameObject.transform.position.y - 5f, 2.5f);

        m_start.GetComponent<CanvasGroup>().DOFade(0f, 2.5f);
        m_twitterLink.GetComponent<CanvasGroup>().DOFade(0f, 2.5f);
        m_kenneyLink.GetComponent<CanvasGroup>().DOFade(0f, 2.5f);

        // Game UI
        m_flowerPots.ForEach(s => s.DOFade(1f, 3f));
        m_seedsPackages.ForEach(s => s.DOFade(1f, 3f));
    }
}