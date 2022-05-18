using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public event Action DiedEvent;
    public event Action<float> HealthChangedEvent;
    public event Action<float> EnergyChangedEvent;
    public event Action<float> ExperienceChangedEvent;
    /// <summary>
    /// param1: current level;
    /// param2: leveling points.
    /// </summary>
    public event Action<int, int> LevelIncreasedEvent;
    public event Action StatsUpdatedEvent;

    public int MaxLevel => _maxLevel;
    public bool HasMaxLevel => level == _maxLevel;

    public float MaxHealth
    {
        get => maxHealth;
        private set => maxHealth = Mathf.Clamp(value, 0, float.MaxValue);
    }
    public float Health
    {
        get => health;
        private set
        {
            health = Mathf.Clamp(value, 0, MaxHealth);
            
            if (Health == 0) DiedEvent?.Invoke();
            
            HealthChangedEvent?.Invoke(Health / MaxHealth);            
        }
    }
    public float Blocking
    {
        get => blocking;
        private set => blocking = Mathf.Clamp(value, 0, float.MaxValue);
    }
    public float HealthRegeneration
    {
        get => healthRegeneration;
        private set => healthRegeneration = Mathf.Clamp(value, 0, _maxHealthRegeneration);
    }
    public float Armor
    {
        get => armor;
        private set => armor = Mathf.Clamp(value, 0, _maxArmor);
    }
    public float AttackDamage
    {
        get => attackDamage;
        private set => attackDamage = Mathf.Clamp(value, 0, float.MaxValue);
    }
    public float ArmorPenetration
    {
        get => armorPenetration;
        private set => armorPenetration = Mathf.Clamp(value, 0, _maxArmorPenetration);
    }
    public float AttackSpeed
    {
        get => attackSpeed;
        private set => attackSpeed = Mathf.Clamp(value, 0, _maxAttackSpeed);
    }
    public float MovementSpeed
    {
        get => movementSpeed;
        private set => movementSpeed = Mathf.Clamp(value, 0, _maxMovementSpeed);
    }
    public float DamageReflection
    {
        get => damageReflection;
        private set => damageReflection = Mathf.Clamp(value, 0, _maxDamageReflection);
    }
    public float LifeStealing
    {
        get => lifeStealing;
        private set => lifeStealing = Mathf.Clamp(value, 0, _maxLifeStealing);
    }
    public int Level
    {
        get => level;
        private set => level = Mathf.Clamp(value, 1, _maxLevel);
    }
    public float Energy
    {
        get => energy;
        private set
        {
            energy = Mathf.Clamp(value, 0, MaxEnergy);
            EnergyChangedEvent?.Invoke(Energy / MaxEnergy);
        }
    }
    public float MaxEnergy
    {
        get => maxEnergy;
        private set => maxEnergy = Mathf.Clamp(value, 0, float.MaxValue);
    }
    public int Experience
    {
        get => experience;
        private set => experience = Mathf.Clamp(value, 0, int.MaxValue);
    }

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float blocking = 50f;
    [SerializeField] private float healthRegeneration = 10f;
    [SerializeField] private float armor = 0f;
    [SerializeField] private float attackDamage = 15f;
    [SerializeField] private float armorPenetration = 0;
    /// <summary>
    /// Number of attacks per second.
    /// </summary>
    [SerializeField] private float attackSpeed = 0.41f;
    [SerializeField] private float movementSpeed = 4.6f;
    [SerializeField] private float damageReflection = 0;
    [SerializeField] private float lifeStealing = 0;
    [SerializeField] private float health = 100;
    [SerializeField] private float energy = 100;
    [SerializeField] private float maxEnergy = 100;
    [SerializeField] private float energyRegeneration = 5f;
    [SerializeField] private int experience = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private int experienceForNextLevel = 15;
    [SerializeField] private int experienceForKilling = 4;
    
    private int _maxLevel = 20;
    private float _maxHealthRegeneration = 25.93f;
    private float _maxLifeStealing = 25.93f;
    private float _maxArmor = 90f;
    private float _maxArmorPenetration = 90f;
    private float _maxAttackSpeed = 2f;
    private float _maxDamageReflection = 90f;
    private float _maxMovementSpeed = 10f;
    private int _levelingPoints = 1;

    private void Start()
    {
        Health = MaxHealth;
        
        // TODO: Bad way.
        HealthChangedEvent?.Invoke(Health);
        EnergyChangedEvent?.Invoke(Energy);
        ExperienceChangedEvent?.Invoke(Experience);
        LevelIncreasedEvent?.Invoke(Level, _levelingPoints);
        
        InvokeRepeating(nameof(RegenerateHealth), 2f, 2f);
        InvokeRepeating(nameof(RegenerateEnergy), 2f, 2f);
    }

    /// <returns>
    /// The damage that was done.
    /// </returns>
    public float TakeDamage(float amount, bool blocked, float penetration, Character attacker)
    {
        float totalArmor = Armor - (Armor / 100 * penetration);
        
        amount -= (amount / 100) * totalArmor;
        if (blocked) amount -= (amount / 100) * Blocking;      
        amount = Mathf.Clamp(amount, 0, float.MaxValue);
        
        Health -= amount;
        
        // Damage reflection.
        if (attacker != null)
        {
            float reflectionDamage = amount / 100 * DamageReflection;
            attacker.TakeDamage(reflectionDamage, ArmorPenetration, null);
        }
        
        // Experience for killing.
        if (Health <= 0 && attacker != null)
            attacker.Stats.AddExperience(experienceForKilling);

        return amount;
    }

    public void Heal(float amount)
    {
        amount = Mathf.Clamp(amount, 0, float.MaxValue);

        Health += amount;
    }

    public void AddToEnergy(float value)
    {
        Energy += value;
    }

    public void AddExperience(int value)
    {
        if (HasMaxLevel) return;
        
        Experience += value;
        
        if (Experience >= experienceForNextLevel)
            TryToLevelUp();
        
        ExperienceChangedEvent?.Invoke((float)Experience / experienceForNextLevel);
    }

    public void TryToLevelUp()
    {
        if (Level == _maxLevel)
            return;
        
        Level++;
        Experience = 0;
        experienceForNextLevel += 5;

        AttackSpeed -= 0.01f;

        _levelingPoints += Level % 12 == 0 ? 2 : 1;
        
        LevelIncreasedEvent?.Invoke(Level, _levelingPoints);
    }

    public void SetMaxLevel(int value)
    {
        _maxLevel = value;
    }

    public void ImproveMaxHealth()
    {
        MaxHealth += 25f;
        StatsUpdatedEvent?.Invoke();
        _levelingPoints--;
    }
    
    public void SetMaxHealth(float value)
    {
        MaxHealth = value;
        StatsUpdatedEvent?.Invoke();
    }

    public void ImproveArmor()
    {
        Armor += level <= 12 ? 3.75f : 1.18f;
        StatsUpdatedEvent?.Invoke();
        _levelingPoints--;
    }
    
    public void ImproveBlocking()
    {
        Blocking += level <= 12 ? 3.75f : 1.18f;
        StatsUpdatedEvent?.Invoke();
        _levelingPoints--;
    }
    
    public void ImproveArmorPenetration()
    {
        ArmorPenetration += level <= 12 ? 3.75f : 1.18f;
        StatsUpdatedEvent?.Invoke();
        _levelingPoints--;
    }

    public void ImproveAttackSpeed()
    {
        AttackSpeed += 0.03f;
        StatsUpdatedEvent?.Invoke();
        _levelingPoints--;
    }
    
    public void ImproveHealthRegeneration()
    {
        HealthRegeneration += level <= 12 ? 1f : 0.34f;
        StatsUpdatedEvent?.Invoke();
        _levelingPoints--;
    }
    
    public void ImproveDamageReflection()
    {
        DamageReflection += level <= 12 ? 3.75f : 1.18f;
        StatsUpdatedEvent?.Invoke();
        _levelingPoints--;
    }
    
    public void ImproveLifeStealing()
    {
        LifeStealing += level <= 12 ? 1f : 0.34f;
        StatsUpdatedEvent?.Invoke();
        _levelingPoints--;
    }

    public void ImproveMovementSpeed()
    {
        MovementSpeed += 0.3f;
        StatsUpdatedEvent?.Invoke();
        _levelingPoints--;
    }
    
    public void ImproveAttackDamage()
    {
        AttackDamage += 2f;
        StatsUpdatedEvent?.Invoke();
        _levelingPoints--;
    }

    public float GetAttribute(Attribute attribute)
    {
        switch (attribute)
        {
            case Attribute.MaxHealth:
                return MaxHealth;
            case Attribute.Armor:
                return Armor;
            case Attribute.Blocking:
                return Blocking;
            case Attribute.ArmorPenetration:
                return ArmorPenetration;
            case Attribute.AttackSpeed:
                return AttackSpeed;
            case Attribute.HealthRegeneration:
                return HealthRegeneration;
            case Attribute.DamageReflection:
                return DamageReflection;
            case Attribute.LifeStealing:
                return LifeStealing;
            case Attribute.MovementSpeed:
                return MovementSpeed;
            case Attribute.AttackDamage:
                return AttackDamage;
            default:
                return -1f;
        }
    }

    private void RegenerateHealth()
    {
        Health += (MaxHealth / 100) * HealthRegeneration;
        StatsUpdatedEvent?.Invoke();
    }
    
    private void RegenerateEnergy()
    {
        Energy += (MaxEnergy / 100) * energyRegeneration;
        StatsUpdatedEvent?.Invoke();
    }
}
