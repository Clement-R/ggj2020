using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    [SerializeField] private List<Sprite> m_sprites;
    [SerializeField] private SpriteRenderer m_sr;

    void Start()
    {
        m_sr.sprite = m_sprites[Random.Range(0, m_sprites.Count - 1)];
    }
}