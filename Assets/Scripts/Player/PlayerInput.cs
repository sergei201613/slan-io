using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, ICharacterInput
{ 
    private Camera _camera;
    private Vector2 _movement;
    
    private void Awake()
    { 
        _camera = Camera.main;
    }

    private void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }

    public Vector3 GetLookTarget()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector2 GetNormalizedMovement()
    {
        return _movement.normalized;
    }

    public bool IsAttacking()
    {
        return Input.GetMouseButton(0);
    }

    public bool IsBlocking()
    {
        return Input.GetMouseButton(1);
    }
}
