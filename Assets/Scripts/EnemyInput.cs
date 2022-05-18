using System;
using UnityEngine;

public class EnemyInput : MonoBehaviour, ICharacterInput
{ 
    private Vector2 _movementDirection = Vector2.zero;
    private Transform _target;
    private Transform _lookTarget;
    private bool _isAttacking = false;

    public void Update()
    {
        _movementDirection =  _target.transform.position - transform.position;
    }

    public Vector3 GetLookTarget()
    {
        return _lookTarget.position;
    }

    public Vector2 GetNormalizedMovement()
    {
        return _movementDirection.normalized;
    }

    public bool IsAttacking()
    {
        return _isAttacking;
    }

    public bool IsBlocking()
    {
        return false;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SetLookTarget(Transform lookTarget)
    {
        _lookTarget = lookTarget;
    }

    public void SetIsAttacking(bool isAttacking)
    {
        _isAttacking = isAttacking;
    }
}
