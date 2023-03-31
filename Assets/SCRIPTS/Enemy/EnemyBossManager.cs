using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class EnemyBossManager : MonoBehaviour
    {
        public string bossName;

        BossHealthBar bossHealthBar;
        EnemyStatsManager enemyStatsManager;
        EnemyAnimatorManager enemyAnimatorManager;
        BossCombatStanceState bossCombatStanceState;

        [Header("Second Phase FX")]
        public GameObject particleFX;
        //Switch Attack Pattern

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<BossHealthBar>();
            enemyStatsManager = GetComponent<EnemyStatsManager>();
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
            bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
        }

        private void Start()
        {
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBoxMaxHealth(enemyStatsManager.maxHealth);
        }

        public void UpdateBossHealthBar(int currentHealth, int maxHealth)
        {
            bossHealthBar.SetBossCurrentHealth(currentHealth);

            if (currentHealth <= maxHealth / 2 && !bossCombatStanceState.hasPhaseShifted)
            {
                bossCombatStanceState.hasPhaseShifted = true;
                ShiftToSecondPhase();
            }
        }

        public void ShiftToSecondPhase()
        {

            // Play second phase animation and trigger particile fx
            enemyAnimatorManager.animator.SetBool("isInvulnerable", true);
            enemyAnimatorManager.animator.SetBool("isPhaseShifting", true);
            enemyAnimatorManager.PlayTargetAnimation("Phase_Shift", true);
            bossCombatStanceState.hasPhaseShifted = true;
            // Switch attack actions
        }
    }
}

