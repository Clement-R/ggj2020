using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FaceTransform : MonoBehaviour
{
    [SerializeField] private Transform m_transform;

    void Update()
    {
        Vector3 pos = m_transform.position;
        Vector3 dir = -pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}