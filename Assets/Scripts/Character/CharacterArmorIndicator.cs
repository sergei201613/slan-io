using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterArmorIndicator : MonoBehaviour
{
    [SerializeField] private GameObject fill;
    [SerializeField] private CharacterStats stats;
    [SerializeField] private PlayerData playerData;

    private void OnEnable()
    {
        if (!playerData.defensiveAttributes.Contains(Attribute.Armor))
        {
            Destroy(gameObject);
            return;
        }
        
        stats.StatsUpdatedEvent += UpdateArmor;
    }
    
    private void OnDisable()
    {
        stats.StatsUpdatedEvent -= UpdateArmor;
    }

    private void UpdateArmor()
    {
        Vector2 fillScale;
        
        fillScale.x = stats.Armor / 100;
        fillScale.y = stats.Armor / 100;
        
        fill.transform.localScale = fillScale;
    }
}
