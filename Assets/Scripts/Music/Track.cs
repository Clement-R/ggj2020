using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Track : MonoBehaviour
{
    public Action<Track> OnTrackChanged;

    public Sample Sample => m_sample;

    public AudioSource Source => m_audioSource;

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
        Sequencer.OnBar += BarChanged;
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

        // Get time from a playing track
        if (m_sequencer.Tracks.Exists(t => t.Source.isPlaying))
        {
            m_audioSource.time = m_sequencer.Tracks.First(t => t.Source.isPlaying).Source.time;
        }
        else
        {
            m_audioSource.time = 0f;
        }

        m_audioSource.Play();
    }
}