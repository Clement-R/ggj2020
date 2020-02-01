using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlanetState : MonoBehaviour
{
    [SerializeField] List<PlanetZone> m_planetZones;

    public EZone DEBUG_ZONE = 0;

    void Start()
    {
        m_planetZones.ForEach(z => z.CurrentState = -1);
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.DownArrow))
        // {
        //     DebugLock();
        // }

        // if (Input.GetKeyDown(KeyCode.UpArrow))
        // {
        //     DebugUnlock();
        // }
    }

    private void LockState(EZone p_zone)
    {
        var planetZone = m_planetZones.Find(z => z.Zone == p_zone);

        if (planetZone.CurrentState < 0)
            return;

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