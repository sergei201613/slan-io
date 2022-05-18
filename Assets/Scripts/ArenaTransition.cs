using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTransition : MonoBehaviour
{
    private SlanGameMode _gameMode;

    private void Awake()
    {
        _gameMode = FindObjectOfType<SlanGameMode>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _gameMode.GoToNextArena(player);
        }
    }
}
