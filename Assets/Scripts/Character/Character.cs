using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public event System.Action Died;
    
    public CharacterStats Stats => stats;
    public int TeamId => teamId;
    
    [SerializeField] protected CharacterStats stats;
    [SerializeField] protected CharacterCombat combat;
    [SerializeField] private int teamId = 0;

    protected bool IsDied = false;
    
    private CharacterMovement _movement;

    protected virtual void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        
        stats.DiedEvent += OnDiedEventCall;
    }

    private void OnDestroy()
    {
        stats.DiedEvent -= OnDiedEventCall;
    }
    
    /// <returns>
    /// The damage that was done.
    /// </returns>
    public float TakeDamage(float amount, float penetration, Character attacker)
    {
        return stats.TakeDamage(amount, combat.IsBlocking, penetration, attacker);
    }

    public void Heal(float amount)
    {
        stats.Heal(amount);
    }

    protected void SetMovementEnabled(bool isEnabled)
    {
        _movement.enabled = isEnabled;
    }

    private void OnDiedEventCall()
    {
        IsDied = true;
        Died?.Invoke();
        OnDied();
    }

    protected virtual void OnDied()
    {
        SetMovementEnabled(false);
        Destroy(gameObject);
    }
}
