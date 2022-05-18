using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterStatsView : MonoBehaviour
    {
        [SerializeField] private Image healthFill = null;
        [SerializeField] private Image energyFill = null;
        [SerializeField] private CharacterStats stats = null;
        [SerializeField] private CircleCollider2D collider = null;

        private RectTransform _rect;
        private float _initialLocalScale;
        private float _initialColliderRadius;
        private float _initialPosY;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            
            _initialLocalScale = _rect.localScale.x;
            _initialColliderRadius = collider.radius;
            _initialPosY = _rect.localPosition.y;
            
            stats.HealthChangedEvent += UpdateHealth;
            stats.EnergyChangedEvent += UpdateEnergy;
        }

        private void OnDestroy()
        {
            stats.HealthChangedEvent -= UpdateHealth;
            stats.EnergyChangedEvent -= UpdateEnergy;
        }

        private void Update()
        {
            var scale = _rect.localScale;
            scale.x = _initialLocalScale + collider.radius - _initialColliderRadius;
            scale.y = _initialLocalScale + collider.radius- _initialColliderRadius;
            _rect.localScale = scale;

            var pos = _rect.localPosition;
            pos.y = _initialPosY + (collider.radius * 1.3f) - _initialColliderRadius;
            _rect.localPosition = pos;
        }

        private void UpdateHealth(float value)
        {
            healthFill.fillAmount = value;
        }

        private void UpdateEnergy(float value)
        {
            energyFill.fillAmount = value;
        }
    }
}
