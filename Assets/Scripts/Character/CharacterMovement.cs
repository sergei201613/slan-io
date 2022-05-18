using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Transform body = null;

    private ICharacterInput _input;
    private CharacterStats _stats;
    private Animator _animator;
    private readonly int _isMoving =  Animator.StringToHash("IsMoving");
    private Vector3 _lastFramePosition = Vector2.zero;

    private void Awake()
    {
        _input = GetComponent<ICharacterInput>();
        _stats = GetComponent<CharacterStats>();
        _animator = GetComponent<Animator>();

        _lastFramePosition = transform.position;
    }

    private void Update()
    {
        var dir = _input.GetLookTarget() - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        body.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        _animator.SetBool(_isMoving, (transform.position - _lastFramePosition).magnitude > 0.005f);

        _lastFramePosition = transform.position;
    }

    private void FixedUpdate()
    {
        float speed = _stats.MovementSpeed;
        rb.MovePosition(rb.position + _input.GetNormalizedMovement() * (speed * Time.fixedDeltaTime));
    }
}
