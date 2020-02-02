using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PlanetState : MonoBehaviour
{
    [SerializeField] List<PlanetZone> m_planetZones;

    public EZone DEBUG_ZONE = 0;

    [SerializeField] private Sequencer m_sequencer;

    void Start()
    {
        m_planetZones.ForEach(z => z.CurrentState = -1);

        m_sequencer.OnTrackChanged += SequencerTrackChanged;
    }

    private void SequencerTrackChanged()
    {
        foreach (var zone in m_planetZones)
        {
            if (zone.Unlocked)
                continue;

            var playingSamples = m_sequencer.PlayingSamples.Distinct().ToList();
            int rightSamples = zone.SamplesToUnlock.Where(s => playingSamples.Contains(s)).ToList().Count;

            if (zone.CurrentState < rightSamples - 1)
            {
                UnlockState(zone.Zone);
            }
            else if (zone.CurrentState > rightSamples - 1)
            {
                LockState(zone.Zone);
            }
        }
    }

    private void LockState(EZone p_zone)
    {
        var planetZone = m_planetZones.Find(z => z.Zone == p_zone);

        planetZone.LockedStates[planetZone.CurrentState].SetActive(true);
        planetZone.UnlockedStates[planetZone.CurrentState].SetActive(false);

        planetZone.CurrentState--;
        planetZone.CurrentState = Mathf.Clamp(planetZone.CurrentState, -1, planetZone.LockedStates.Length - 1);
    }

    private void UnlockState(EZone p_zone)
    {
        var planetZone = m_planetZones.Find(z => z.Zone == p_zone);

        planetZone.CurrentState++;
        planetZone.CurrentState = Mathf.Clamp(planetZone.CurrentState, -1, planetZone.UnlockedStates.Length - 1);

        planetZone.LockedStates[planetZone.CurrentState].SetActive(false);
        planetZone.UnlockedStates[planetZone.CurrentState].SetActive(true);

        if (planetZone.CurrentState == planetZone.UnlockedStates.Length - 1)
        {
            planetZone.Unlocked = true;
        }
    }

    [ContextMenu("Debug lock")]
    private void DebugLock()
    {
        LockState(DEBUG_ZONE);
    }

    [ContextMenu("Debug unlock")]
    private void DebugUnlock()
    {
        UnlockState(DEBUG_ZONE);
    }
}