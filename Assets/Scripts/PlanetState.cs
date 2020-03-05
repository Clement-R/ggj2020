using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PlanetState : MonoBehaviour
{
    public static PlanetState Instance;

    [SerializeField] List<PlanetZone> m_planetZones;
    [SerializeField] int m_numberOfLockedStates = 3;

    public EZone DEBUG_ZONE = 0;

    [SerializeField] private Sequencer m_sequencer;

    private Dictionary<EZone, Dictionary<int, List<GameObject>>> m_props = new Dictionary<EZone, Dictionary<int, List<GameObject>>>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        // Reset planet zones
        m_planetZones.ForEach(z => z.CurrentState = -1);
        m_sequencer.OnTrackChanged += SequencerTrackChanged;
    }

    public void RegisterProp(WorldProp p_prop)
    {
        if (m_props.ContainsKey(p_prop.Zone) == false)
        {
            m_props.Add(p_prop.Zone, new Dictionary<int, List<GameObject>>());
        }

        if (m_props[p_prop.Zone].ContainsKey(p_prop.State) == false)
        {
            m_props[p_prop.Zone].Add(p_prop.State, new List<GameObject>());
        }

        m_props[p_prop.Zone][p_prop.State].Add(p_prop.gameObject);
        p_prop.gameObject.SetActive(false);
    }

    private void SequencerTrackChanged()
    {
        // Check for each zone if the right samples are playing to unlock it
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

        m_props[p_zone][planetZone.CurrentState].ForEach(p => p.SetActive(false));

        planetZone.CurrentState--;
        planetZone.CurrentState = Mathf.Clamp(planetZone.CurrentState, -1, m_numberOfLockedStates - 1);
    }

    private void UnlockState(EZone p_zone)
    {
        var planetZone = m_planetZones.Find(z => z.Zone == p_zone);

        planetZone.CurrentState++;
        planetZone.CurrentState = Mathf.Clamp(planetZone.CurrentState, -1, m_numberOfLockedStates - 1);

        m_props[p_zone][planetZone.CurrentState].ForEach(p => p.SetActive(true));

        if (planetZone.CurrentState == m_numberOfLockedStates - 1)
        {
            planetZone.Unlocked = true;

            // Play all particle systems when unlocking whole zone
            for (int i = 0; i < m_props[p_zone].Count; i++)
            {
                foreach (var go in m_props[p_zone][i])
                {
                    if (go.TryGetComponent(out ParticleSystem particleSystem))
                    {
                        particleSystem.Play();
                        Debug.Log($"Play {particleSystem.gameObject.name}");
                    }
                }
            }
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