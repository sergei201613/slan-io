using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArenaChangedNotification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI arenaChangedText;
    [SerializeField] private Image image;
    [SerializeField] private float lerpSpeed = 5f;

    private float _targetAlpha = 0f;

    private void Update()
    {
        Color color = image.color;
        Color textColor = arenaChangedText.color;
        
        color.a = Mathf.Lerp(image.color.a, _targetAlpha, lerpSpeed * Time.deltaTime);
        textColor.a = Mathf.Lerp(arenaChangedText.color.a, _targetAlpha, lerpSpeed * Time.deltaTime);
        
        image.color = color;
        arenaChangedText.color = textColor;
    }

    public void Show(string text)
    {
        arenaChangedText.text = text;
        
        gameObject.SetActive(true);
        _targetAlpha = .75f;
        
        Invoke(nameof(Hide), 2);
    }
    
    public void Hide()
    {
        _targetAlpha = 0f;
    }
}
