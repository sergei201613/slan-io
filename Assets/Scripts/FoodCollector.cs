using UnityEngine;

public class FoodCollector : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var food = other.gameObject.GetComponent<Food>();

        if (food != null)
        {
            stats.AddExperience(food.Experience);
            Destroy(food.gameObject);
        }
    }
}
