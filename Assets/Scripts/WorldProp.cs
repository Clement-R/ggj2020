using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class WorldProp : MonoBehaviour
{
    public EZone Zone => m_zone;
    public int State => m_layer;

    [SerializeField] private EZone m_zone;
    [SerializeField] private int m_layer;

    private void Start()
    {
        PlanetState.Instance.RegisterProp(this);
    }
}