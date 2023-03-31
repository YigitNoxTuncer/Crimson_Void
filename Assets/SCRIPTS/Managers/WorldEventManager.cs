using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NOX
{
    public class WorldEventManager : MonoBehaviour
    {
        public List<FogWall> fogWalls;
        BossHealthBar bossHealthBar;
        EnemyBossManager boss;
        GameOverScreen gameOverScreen;
        public Text gameOverScreenText;

        public bool bossFightIsActive; //currently fighting boss
        public bool bossHasBeenAwakaned; // watched cutscene before died
        public bool bossHasBeenDefeated;

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<BossHealthBar>();
            gameOverScreen = FindObjectOfType<GameOverScreen>();
        }
        private void Update()
        {
            if (bossHasBeenDefeated)
            {
                BossHasBeenDefeated();
            }
        }

        public void ActivateBossFight()
        {
            bossFightIsActive = true;
            bossHasBeenAwakaned = true;
            bossHealthBar.SetUIHealthBarToActive();

            foreach (var fogWall in fogWalls)
            {
                fogWall.ActivateFogWall();
            }

        }

        public void BossHasBeenDefeated()
        {
            /*
            bossHasBeenDefeated = true;
            bossFightIsActive = false;

            foreach (var fogWall in fogWalls)
            {
                fogWall.DeactivateFogWall();
            }
            */

            gameOverScreen.gameObject.SetActive(true);
            gameOverScreenText.text = "CHIEFTAIN SLAIN!";
        }
    }
}

