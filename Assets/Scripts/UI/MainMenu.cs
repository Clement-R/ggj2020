using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button m_start;
    [SerializeField] private Button m_twitterLink;
    [SerializeField] private Button m_kenneyLink;

    [Header("Texts")]
    [SerializeField] private CanvasGroup m_name;
    [SerializeField] private CanvasGroup m_subName;

    [Header("Game UI")]
    [SerializeField] private List<SpriteRenderer> m_flowerPots;
    [SerializeField] private List<CanvasGroup> m_seedsPackages;

    [Header("Sounds")]
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_vinylNoise;
    [SerializeField] private AudioClip m_startPlaying;

    private void Start()
    {
        //TODO: link button actions
    }

    private void Play()
    {
        StartCoroutine(_Play());
    }

    private IEnumerator _Play()
    {
        yield return null;
        // m_audioSource
    }
}