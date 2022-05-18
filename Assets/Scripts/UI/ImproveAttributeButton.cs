using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImproveAttributeButton : MonoBehaviour
{
    public String Text
    {
        get => text.text;
        set => text.text = value;
    }
    
    [SerializeField] private Attribute attribute;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button improveButton;
    [SerializeField] private PlayerStatsView playerStatsView;

    private void Awake()
    {
        playerStatsView = GetComponentInParent<PlayerStatsView>();
        playerStatsView.RegisterImproveButton(this);
    }

    public void OnClick()
    {
        playerStatsView.ImproveAttribute(attribute);
    }

    public void SetActiveImproveButton(bool active)
    {
        improveButton.gameObject.SetActive(active);
    }
}

public enum Attribute
{
     MaxHealth,
     Armor,
     Blocking,
     ArmorPenetration,
     AttackSpeed,
     HealthRegeneration,
     DamageReflection,
     LifeStealing,
     MovementSpeed,
     AttackDamage
}
