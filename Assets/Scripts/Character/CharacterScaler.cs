using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScaler : MonoBehaviour
{
    public event System.Action<float> Scaled; 
    
    [SerializeField] private ArmsController armsController;
    [SerializeField] private Transform body;
    [SerializeField] private CharacterStats stats;
    [SerializeField] private CircleCollider2D bodyCollider;

    private Vector3 _bodyInitialScale;
    private float _armInitialScale;
    private float _bodyColliderInitialRadius;
    private float _initialMaxHealth;

    private void Awake()
    {
        _bodyInitialScale = body.localScale;
        _armInitialScale = armsController.ArmScale;
        _bodyColliderInitialRadius = bodyCollider.radius;
        _initialMaxHealth = stats.MaxHealth;

        UpdateScale();
    }

    private void OnEnable()
    {
        stats.StatsUpdatedEvent += UpdateScale;
    }

    private void OnDisable()
    {
        stats.StatsUpdatedEvent -= UpdateScale;
    }
    
    private void UpdateScale()
    {
        ScaleBody();
        ScaleArms();
    }

    private void ScaleBody()
    {
        body.localScale = _bodyInitialScale * stats.MaxHealth / _initialMaxHealth;

        bodyCollider.radius = _bodyColliderInitialRadius * (body.localScale.x / _bodyInitialScale.x);

        Scaled?.Invoke(body.localScale.x);
    }

    private void ScaleArms()
    {
        armsController.SetArmsScale(_armInitialScale * stats.AttackDamage / 15f);
    }
}
