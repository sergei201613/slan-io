using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsView : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TMP_Text nick;
    
    // Defensive attributes.
    [SerializeField] private ImproveAttributeButton maxHealth;
    [SerializeField] private ImproveAttributeButton blocking;
    [SerializeField] private ImproveAttributeButton healthRegeneration;
    [SerializeField] private ImproveAttributeButton armor;
    
    // Offensive attributes.
    [SerializeField] private ImproveAttributeButton attackDamage;
    [SerializeField] private ImproveAttributeButton armorPenetration;
    [SerializeField] private ImproveAttributeButton attackSpeed;
    
    // Special attributes.
    [SerializeField] private ImproveAttributeButton movementSpeed;
    [SerializeField] private ImproveAttributeButton damageReflection;
    [SerializeField] private ImproveAttributeButton lifeStealing;
    
    private CharacterStats _stats;
    private readonly List<ImproveAttributeButton> _improveButtons = new List<ImproveAttributeButton>();
    private readonly Dictionary<Attribute, ImproveAttributeButton> _defensiveAttributes = new Dictionary<Attribute, ImproveAttributeButton>();
    private int _levelingPoints = 0;
    
    private void Awake()
    {
        _stats = FindObjectOfType<Player>().GetComponent<CharacterStats>();

        UpdateStatsInfo();

        _stats.StatsUpdatedEvent += UpdateStatsInfo;
        _stats.LevelIncreasedEvent += OnLevelUp;
    }

    private void Start()
    {
        SetActiveImproveButtons(false);

        nick.text = playerData.nickname;
        
        _defensiveAttributes.Add(Attribute.MaxHealth, maxHealth);
        _defensiveAttributes.Add(Attribute.Blocking, blocking);
        _defensiveAttributes.Add(Attribute.HealthRegeneration, healthRegeneration);
        _defensiveAttributes.Add(Attribute.Armor, armor);

        foreach (var attribute in playerData.defensiveAttributes)
        {
            _defensiveAttributes[attribute].gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        _stats.StatsUpdatedEvent -= UpdateStatsInfo;
        _stats.LevelIncreasedEvent -= OnLevelUp;
    }
    
    public void ImproveAttribute(Attribute attribute)
    {
        switch (attribute)
        {
            case Attribute.MaxHealth:
                _stats.ImproveMaxHealth();
                break;
            case Attribute.Armor:
                _stats.ImproveArmor();
                break;
            case Attribute.Blocking:
                _stats.ImproveBlocking();
                break;
            case Attribute.ArmorPenetration:
                _stats.ImproveArmorPenetration();
                break;
            case Attribute.AttackSpeed:
                _stats.ImproveAttackSpeed();
                break;
            case Attribute.HealthRegeneration:
                _stats.ImproveHealthRegeneration();
                break;
            case Attribute.DamageReflection:
                _stats.ImproveDamageReflection();
                break;
            case Attribute.LifeStealing:
                _stats.ImproveLifeStealing();
                break;
            case Attribute.MovementSpeed:
                _stats.ImproveMovementSpeed();
                break;
            case Attribute.AttackDamage:
                _stats.ImproveAttackDamage();
                break;
        }
        
        _levelingPoints--;

        if (_levelingPoints < 1)
            SetActiveImproveButtons(false);
    }

    public void RegisterImproveButton(ImproveAttributeButton attributeButton)
    {
        _improveButtons.Add(attributeButton);
    }
    
    private void UpdateStatsInfo()
    {
        maxHealth.Text = _stats.MaxHealth.ToString("0.00");
        blocking.Text = _stats.Blocking.ToString("0.00") + "%";
        healthRegeneration.Text = _stats.HealthRegeneration.ToString("0.00") + "%";
        armor.Text = _stats.Armor.ToString("0.00") + "%";

        attackDamage.Text = _stats.AttackDamage.ToString("0.00");
        armorPenetration.Text = _stats.ArmorPenetration.ToString("0.00") + "%";
        attackSpeed.Text = _stats.AttackSpeed.ToString("0.00") + "s";
            
        movementSpeed.Text = _stats.MovementSpeed.ToString("0.00");
        damageReflection.Text = _stats.DamageReflection.ToString("0.00") + "%";
        lifeStealing.Text = _stats.LifeStealing.ToString("0.00") + "%";
    }

    private void SetActiveImproveButtons(bool active)
    {
        foreach (var button in _improveButtons)
            button.SetActiveImproveButton(active);
    }

    private void OnLevelUp(int level, int points)
    {
        _levelingPoints = points;
        
        SetActiveImproveButtons(true);
    }
}
