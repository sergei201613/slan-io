using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Arena arena;
    [SerializeField] private float spawnDelay;
    //[SerializeField] private int maxFoodCount = 100;
    
    //private int _foodCount = 0;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnDelay, spawnDelay);

        for (int i = 0; i < 150; i++) Spawn();
    }

    private void Spawn()
    {
        //if (_foodCount >= maxFoodCount) return;

        Vector2 position;
        position.x = Random.Range(arena.Bounds.min.x, arena.Bounds.max.x);
        position.y = Random.Range(arena.Bounds.min.y, arena.Bounds.max.y);
        
        Instantiate(foodPrefab, position, Quaternion.identity);
    }
}
