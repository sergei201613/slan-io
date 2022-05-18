using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlanGameMode : GameModeBase
{
    public event System.Action GameOverEvent;
    public event System.Action<int> ArenaChangedEvent;
    
    public Arena Arena => arena;
    
    [SerializeField] private Arena arena;
    
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        _player.Died += GameOver;
    }
    
    private void OnDisable()
    {
        _player.Died -= GameOver;
        
        Time.timeScale = 1;
    }


    public void GoToNextArena(Player player)
    {
        player.Stats.SetMaxLevel(player.Stats.Level + 20);

        var playerTransform = player.transform;
        var playerPos = playerTransform.position;

        playerTransform.position = new Vector3(
            Arena.Bounds.min.x,
            playerPos.y,
            playerPos.z);
        
        arena.GoToNextArena();
        ArenaChangedEvent?.Invoke(arena.ArenaNumber);
    }

    private void GameOver()
    {
        GameOverEvent?.Invoke();
        Time.timeScale = 0;
    }
}
