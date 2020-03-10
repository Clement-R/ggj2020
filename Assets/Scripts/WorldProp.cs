using UnityEngine;

using DG.Tweening;

public class WorldProp : MonoBehaviour
{
    public EZone Zone => m_zone;
    public int State => m_layer;

    [SerializeField] private EZone m_zone;
    [SerializeField] private int m_layer;

    protected virtual void Start()
    {
        PlanetState.Instance.RegisterProp(this);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}