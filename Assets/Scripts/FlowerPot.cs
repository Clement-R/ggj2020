using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Lean.Touch;

public class FlowerPot : MonoBehaviour
{
    public Action<Sample> OnSampleChange;
    [SerializeField] List<GameObject> m_outlineParts;
    [SerializeField] SpriteRenderer m_flowerRenderer;

    private bool m_outlined = false;
    private SeedsPackage m_hoveringPackage = null;

    void Start()
    {
        LeanTouch.OnFingerUp += FingerUp;
    }

    private void FingerUp(LeanFinger p_finger)
    {
        if (m_hoveringPackage != null)
        {
            OnSampleChange?.Invoke(m_hoveringPackage.Sample);
            UpdateFlower();
        }
    }

    private void UpdateFlower()
    {
        //TODO: Get seed flower's sprite and change for it
        // m_flowerRenderer.sprite = 
    }

    private void UpdateOutline()
    {
        m_outlineParts.ForEach(o => o.SetActive(m_outlined));
    }

    private void OnTriggerEnter2D(Collider2D p_collider)
    {
        if (p_collider.gameObject.CompareTag("SeedsPackage"))
        {
            m_hoveringPackage = p_collider.gameObject.GetComponent<SeedsPackage>();

            m_outlined = true;
            UpdateOutline();
        }
    }

    private void OnTriggerExit2D(Collider2D p_collider)
    {
        if (p_collider.gameObject.CompareTag("SeedsPackage") && m_outlined)
        {
            m_hoveringPackage = null;

            m_outlined = false;
            UpdateOutline();
        }
    }
}