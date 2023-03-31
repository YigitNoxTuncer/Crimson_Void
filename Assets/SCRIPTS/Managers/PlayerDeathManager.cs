using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class PlayerDeathManager : MonoBehaviour
    {
        PlayerStatsManager playerStats;
        PlayerManager playerManager;
        GameOverScreen gameOverScreen;

        private void Awake()
        {
            playerStats = GetComponent<PlayerStatsManager>();
            playerManager = GetComponent<PlayerManager>();
            gameOverScreen = FindObjectOfType<GameOverScreen>();
        }

        public void GameOverScreenEnabled()
        {
            gameOverScreen.gameObject.SetActive(true);
        }

        public void GameOverScreenDisabled()
        {
            gameOverScreen.gameObject.SetActive(false);
        }
    }
}

