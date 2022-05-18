using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private ExperienceView experienceView = null;
        [SerializeField] private CharacterStats stats = null;
        [SerializeField] private PlayerStatsView playerStats = null;
        [SerializeField] private GameObject gameOverPanel = null;
        [SerializeField] private GameObject maxLevelWarningPanel = null;
        [SerializeField] private ArenaChangedNotification arenaChangedNotification = null;

        private SlanGameMode _gameMode;
        
        private void Awake()
        {
            if (stats == null) stats = FindObjectOfType<Player>().Stats;
            _gameMode = FindObjectOfType<SlanGameMode>();
            
            experienceView.Init(stats);
        }

        private void OnEnable()
        {
            _gameMode.GameOverEvent += OnGameOver;
            _gameMode.ArenaChangedEvent += OnArenaChanged;
            stats.LevelIncreasedEvent += OnLevelUp;
        }
        
        private void OnDisable()
        {
            _gameMode.GameOverEvent -= OnGameOver;
            _gameMode.ArenaChangedEvent -= OnArenaChanged;
            stats.LevelIncreasedEvent -= OnLevelUp;
        }

        private void OnGameOver()
        {
            experienceView.gameObject.SetActive(false);
            playerStats.gameObject.SetActive(false);
            gameOverPanel.SetActive(true);
        }

        private void OnLevelUp(int level, int points)
        {
            maxLevelWarningPanel.SetActive(stats.HasMaxLevel);
        }

        private void OnArenaChanged(int index)
        {
            maxLevelWarningPanel.SetActive(false);
            arenaChangedNotification.Show("Arena " + index);
        }
    }
}
