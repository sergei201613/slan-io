using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int Experience => experience;
    
    [SerializeField] private int experience = 1;
    [SerializeField] private Color[] colors;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _spriteRenderer.color = colors[UnityEngine.Random.Range(0, colors.Length)];
    }
}
