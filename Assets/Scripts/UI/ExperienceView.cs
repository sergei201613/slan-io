using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class ExperienceView : MonoBehaviour
    {
        [SerializeField] private Image fill = null;
        [SerializeField] private TextMeshProUGUI levelText = null;
        
        private CharacterStats _stats;
        
        public void Init(CharacterStats stats)
        {
            _stats = stats;

            _stats.ExperienceChangedEvent += UpdateExperience;
            _stats.LevelIncreasedEvent += UpdateLevel;
        }

        private void OnDestroy()
        {
            _stats.ExperienceChangedEvent -= UpdateExperience;
            _stats.LevelIncreasedEvent -= UpdateLevel;
        }

        private void UpdateExperience(float value)
        {
            fill.fillAmount = value;
        }
        
        private void UpdateLevel(int level, int points)
        {
            levelText.text = level.ToString();
        }
    }
}
