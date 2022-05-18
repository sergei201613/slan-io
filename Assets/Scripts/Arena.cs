using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public Collider2D Area => area;
    public Bounds Bounds => area.bounds;
    public int MaxLevel => maxLevel;
    public int ArenaNumber => arenaNumber;

    [SerializeField] private int arenaNumber = 1;
    [SerializeField] private Collider2D area;
    /// <summary>
    /// If the character's level is equal to the maximum, he will
    /// not be able to gain experience in this arena.
    /// </summary>
    [SerializeField] private int maxLevel = 20;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Color[] backgroundColors;

    private readonly List<Enemy> _enemies = new List<Enemy>();

    private void Start()
    {
        ChangeBackgroundColor();
    }

    public void RegisterEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void GoToNextArena()
    {
        arenaNumber++;
        
        ClearEnemies();
        ChangeBackgroundColor();
        enemySpawner.ReduceChanceOfPurpleEnemiesAppearing();
    }
    
    private void ChangeBackgroundColor()
    {
        if (arenaNumber <= backgroundColors.Length)
            background.color = backgroundColors[arenaNumber - 1];
    }

    private void ClearEnemies()
    {
        foreach (var enemy in _enemies)
        {
            if (enemy != null)
                Destroy(enemy.gameObject);
        }      
        
        _enemies.Clear();
    }
}
