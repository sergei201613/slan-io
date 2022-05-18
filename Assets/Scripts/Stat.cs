using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float baseValue = 0;
    [SerializeField] private float maxValue = 100;

    private List<float> _modifiers = new List<float>();

    public float GetValue()
    {
        float finalValue = baseValue;
        _modifiers.ForEach(f => finalValue += f);
        
        return finalValue;
    }

    public void AddToBaseValue(float value)
    {
        baseValue = Mathf.Clamp(baseValue + value, 0f, float.MaxValue);
    }

    public void SetBaseValue(float value)
    {
        baseValue = Mathf.Clamp(value, 0f, maxValue);
    }

    public void SetMaxValue(float value)
    {
        maxValue = Mathf.Clamp(value, 0, float.MaxValue);
    }

    public void AddModifier(float modifier)
    {
        if (modifier != 0)
            _modifiers.Add(modifier);
    }

    public void RemoveModifier(float modifier)
    {
        _modifiers.Remove(modifier);
    }
}
