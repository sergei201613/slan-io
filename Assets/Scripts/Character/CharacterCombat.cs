using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public bool IsBlocking => _input.IsBlocking() && HasEnergy;
    public bool HasEnemiesNearby => _enemiesInAttackRange.Count > 0;
    
    [SerializeField] private Animator animator;
    [SerializeField] private float energyPerAttack = 15f;
    [SerializeField] private float energyPerBlock = 15f;

    private bool HasEnergy => _stats.Energy > 0;
    private bool CanAttack => (_lastAttackTime + 1 / _stats.AttackSpeed) < Time.time && HasEnergy && !_input.IsBlocking();
    private ICharacterInput _input;
    private CharacterStats _stats;
    private Character _character;
    private readonly HashSet<Character> _enemiesInAttackRange = new HashSet<Character>();
    private float _lastAttackTime = 0;
    private float _lastTimeEnergyDecreasedByBlocking = 0;
    private bool _isLeftAttack = true;
    private readonly int _animLeftAttack = Animator.StringToHash("LeftAttack");
    private readonly int _animRightAttack = Animator.StringToHash("RightAttack");
    private readonly int _animBlocking = Animator.StringToHash("IsBlocking");
    
    private void Awake()
    {
        _stats = GetComponent<CharacterStats>()
                 ?? GetComponentInParent<CharacterStats>()
                 ?? GetComponentInChildren<CharacterStats>();
        
        _character = GetComponent<Character>()
                 ?? GetComponentInParent<Character>()
                 ?? GetComponentInChildren<Character>();
        
        _input = GetComponent<ICharacterInput>()
                 ?? GetComponentInParent<ICharacterInput>()
                 ?? GetComponentInChildren<ICharacterInput>();
    }

    private void Update()
    {
        if (_input.IsAttacking() && CanAttack)
        {
            foreach (var character in _enemiesInAttackRange)
            {
                float damageDone = character.TakeDamage(_stats.AttackDamage, _stats.ArmorPenetration, _character);
                _character.Heal(damageDone / 100 * _stats.LifeStealing);
                
                
                break;
            }

            _stats.AddToEnergy(-energyPerAttack);
            
            _lastAttackTime = Time.time;
            
            animator.SetTrigger(_isLeftAttack ? _animLeftAttack : _animRightAttack);
            _isLeftAttack = !_isLeftAttack;
        }
        
        if (IsBlocking)
        {
            if (Time.time - _lastTimeEnergyDecreasedByBlocking > 2f)
            {
                _stats.AddToEnergy(-energyPerBlock);
                _lastTimeEnergyDecreasedByBlocking = Time.time;
            }
        }
        
        animator.SetBool(_animBlocking, IsBlocking);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var character = other.gameObject.GetComponent<Character>();

        if (character != null && _character.gameObject != character.gameObject && _character.TeamId != character.TeamId)
        {
            _enemiesInAttackRange.Add(character);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var character = other.gameObject.GetComponent<Character>();

        if (character != null)
        {
            _enemiesInAttackRange.Remove(character);
        }
    }
}
