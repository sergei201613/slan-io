using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyPrefabs = new List<Enemy>();
    [SerializeField] private float spawnDelay = 3f;
    [SerializeField] private Arena arena;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnDelay, spawnDelay);
    }

    public void ReduceChanceOfPurpleEnemiesAppearing()
    {
        if (arena.ArenaNumber % 3 == 0)
            enemyPrefabs.Add(enemyPrefabs[0]);
        else if (arena.ArenaNumber % 4 == 0)
            enemyPrefabs.Add(enemyPrefabs[1]);
    }

    private void Spawn()
    {
        int prefabIndex = UnityEngine.Random.Range(0, enemyPrefabs.Count);
        
        Vector2 position;
        position.x = Random.Range(arena.Bounds.min.x, arena.Bounds.max.x);
        position.y = Random.Range(arena.Bounds.min.y, arena.Bounds.max.y);
        
        var enemy = Instantiate(enemyPrefabs[prefabIndex], position, Quaternion.identity);
        
        // Only for purple enemies.
        if (prefabIndex == 2)
            enemy.Stats.SetMaxHealth(enemy.Stats.MaxHealth + (arena.ArenaNumber * 10) - 10);
        
        arena.RegisterEnemy(enemy);
    }
}
