using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[ExecuteInEditMode]
public class FaceTransform : MonoBehaviour
{
    [SerializeField] private Transform m_transform;

    void Update()
    {
        if (m_transform == null) return;

        transform.up = (transform.position - m_transform.position).normalized;
    }
}