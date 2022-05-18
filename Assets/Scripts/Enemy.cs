using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Tooltip("Used to ignore collisions of other enemies")]
    [SerializeField] private int layerMaskId;
    [Tooltip("The distance at which the enemy will not follow the player")]
    [SerializeField] private float stopDistance = 1.8f;
    
    private Transform _player;
    private EnemyInput _input;

    protected override void Awake()
    {
        base.Awake();
        
        _player = FindObjectOfType<Player>().transform;
        _input = GetComponent<EnemyInput>();
        
        _input.SetTarget(_player);
        _input.SetLookTarget(_player);
    }

    private void Update()
    {
        _input.SetIsAttacking(combat.HasEnemiesNearby);

        if (IsDied)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 15 * Time.deltaTime);
            if (transform.localScale == Vector3.zero)
                Destroy(gameObject);
        }
    }

    protected override void OnDied()
    {
        SetMovementEnabled(false);
    }
}
