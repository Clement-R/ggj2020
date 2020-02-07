using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Track : MonoBehaviour
{
    public Action<Track> OnTrackChanged;

    public Sample Sample => m_sample;

    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private FlowerPot m_flowerPot;

    private Sequencer m_sequencer;
    private Sample m_sample;

    private bool m_waitForBarToStart = false;

    void Start()
    {
        m_flowerPot.OnSampleChange += SampleChange;
        m_flowerPot.OnSampleRemoved += SampleRemoved;
    }

    public void Register(Sequencer p_sequencer)
    {
        m_sequencer = p_sequencer;
        m_sequencer.OnBar += BarChanged;
    }

    public void SampleChange(Sample p_sample)
    {
        m_sample = p_sample;
        m_waitForBarToStart = true;
        OnTrackChanged?.Invoke(this);
    }

    private void SampleRemoved()
    {
        m_sample = null;
        m_audioSource.Stop();
        OnTrackChanged?.Invoke(this);
    }

    private void BarChanged()
    {
        if (m_waitForBarToStart)
        {
            m_waitForBarToStart = false;
            Play();
        }
    }

    public void Play()
    {
        if (m_sample == null)
            return;

        m_audioSource.clip = m_sample.Clip;
        m_audioSource.time = m_sequencer.CyclePosition;
        m_audioSource.Play();
    }
}