using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Camera camera;

    private CharacterScaler _scaler;
    private float _cameraInitialSize;

    protected override void Awake()
    {
        base.Awake();

        _scaler = GetComponent<CharacterScaler>();
        
        _cameraInitialSize = camera.orthographicSize;
    }

    private void OnEnable()
    {
        _scaler.Scaled += OnScaled;
    }
    
    private void OnDisable()
    {
        _scaler.Scaled -= OnScaled;
    }

    private void OnScaled(float scale)
    {
        camera.orthographicSize = _cameraInitialSize * scale * 2;
    }

    protected override void OnDied()
    {
        SetMovementEnabled(false);
    }
}
