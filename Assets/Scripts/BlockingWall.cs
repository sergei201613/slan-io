using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingWall : MonoBehaviour
{
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Collider2D thisCollider;

    private void Start()
    {
        Physics2D.IgnoreCollision(thisCollider, playerCollider);
    }
}
