using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Sequencer : MonoBehaviour
{
    public static Action OnBar;
    public static Action OnBeat;
    public static Action OnStop;

    public Action OnTrackChanged;

    public float CyclePosition => m_cyclePosition;
    public float CycleProgression => m_cycleProgression;
    public float CycleDuration => m_cycleDuration;
    public int CurrentBar => m_currentBar;
    public float BarDuration => m_barDurationInSeconds;
    public float BarProgression => m_barProgression;
    public float BarPosition => m_barDurationInSeconds * m_barProgression;

    public List<Sample> PlayingSamples => m_tracks.Where(t => t.Sample != null).Select(t => t.Sample).ToList();

    [SerializeField] private float m_bpm;
    [SerializeField] private List<Track> m_tracks;

    private float m_barDurationInSeconds => m_beatDurationInSeconds * 4f;
    private float m_beatDurationInSeconds => (60000f / m_bpm) / 1000f;
    private float m_barProgression => (Time.time - m_barStart) / m_barDurationInSeconds;
    private float m_cycleDuration => m_barDurationInSeconds * 4f;
    private float m_cyclePosition => ((m_currentBar - 1) * m_barDurationInSeconds) + (m_barDurationInSeconds * m_barProgression);
    private float m_cycleProgression => m_cyclePosition / m_cycleDuration;
    private float m_barStart = 0f;
    private float m_beatStart = 0f;
    private bool m_isPlaying = false;
    private int m_currentBar = 1;

    void Start()
    {
        m_tracks.ForEach(t => RegisterTrack(t));
    }

    private void RegisterTrack(Track p_track)
    {
        p_track.Register(this);
        p_track.OnTrackChanged += TrackSampleChanged;
    }

    private void TrackSampleChanged(Track p_track)
    {
        if (m_isPlaying == false)
        {
            Play();
        }
        else if (p_track.Sample == null)
        {
            CheckIfShouldStop();
        }

        OnTrackChanged?.Invoke();
    }

    void Update()
    {
        if (m_isPlaying == false)
            return;

        if (Time.time >= m_beatStart + m_beatDurationInSeconds)
        {
            Beat();
        }

        if (Time.time >= m_barStart + m_barDurationInSeconds)
        {
            Bar();
        }
    }

    [ContextMenu("Play debug")]
    private void Play()
    {
        m_isPlaying = true;
        m_tracks.ForEach(t => t.Play());

        m_currentBar = 0;
        Bar();
        Beat();
    }

    private void Beat()
    {
        m_beatStart = Time.time;
        OnBeat?.Invoke();
    }

    private void Bar()
    {
        m_currentBar++;
        if (m_currentBar > 4)
            m_currentBar = 1;

        m_barStart = Time.time;
        OnBar?.Invoke();
    }

    private void CheckIfShouldStop()
    {
        if (m_tracks.All(t => t.Sample == null))
        {
            m_isPlaying = false;
            m_currentBar = 0;
            OnStop?.Invoke();
        }
    }
}