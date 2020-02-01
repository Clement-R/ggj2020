using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Lean.Touch;

public class FlowerPot : MonoBehaviour
{
    [SerializeField] List<GameObject> m_outlineParts;

    private bool m_outlined = false;

    void Start()
    {

    }

    void Update()
    {

    }

    private void UpdateOutline()
    {
        m_outlineParts.ForEach(o => o.SetActive(m_outlined));
    }

    private void OnTriggerEnter2D(Collider2D p_collider)
    {
        if (p_collider.gameObject.CompareTag("SeedsPackage"))
        {
            m_outlined = true;
            UpdateOutline();
        }
    }

    private void OnTriggerExit2D(Collider2D p_collider)
    {
        if (p_collider.gameObject.CompareTag("SeedsPackage") && m_outlined)
        {
            m_outlined = false;
            UpdateOutline();
        }
    }
}