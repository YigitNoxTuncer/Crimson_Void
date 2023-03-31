using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOX
{
    public class EnemyStatsManager : CharacterStatsManager
    {
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;
        EnemyBossManager enemyBossManager;
        WorldEventManager worldEventManager;
        public UIEnemyHealthBar enemyHealthBar;

        //public bool isBoss;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
            enemyBossManager = GetComponent<EnemyBossManager>();
            worldEventManager = FindObjectOfType<WorldEventManager>();
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        void Start()
        {
            if (!isBoss)
            {
                enemyHealthBar.SetMaxHealth(maxHealth);
            }
        }

        public override void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer = poiseResetTimer - Time.deltaTime;
            }
            else if (poiseResetTimer <= 0 && !enemyManager.isInteracting)
            {
                totalPoiseDefense = armorPoiseBonus;
            }
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public override void TakeDamageNoAnimation(int physicalDamage, int fireDamage)
        {
            base.TakeDamageNoAnimation(physicalDamage, fireDamage);

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }

            if (currentHealth <= 0 && isBoss)
            {
                worldEventManager.bossHasBeenDefeated = true;
            }
        }

        public override void TakePoisonDamage(int damage)
        {
            if (isDead)
                return;

            base.TakePoisonDamage(damage);

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }

            if (currentHealth <= 0 && isBoss)
            {
                worldEventManager.bossHasBeenDefeated = true;
            }


            if (currentHealth <= 0)
            {
                currentHealth = 0;
                enemyAnimatorManager.PlayTargetAnimation("Death", true);
                isDead = true;

            }
        }
        
        public void BreakGuard()
        {
            enemyAnimatorManager.PlayTargetAnimation("Break_Guard", true);
        }

        public override void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation = "Damage_1")
        {
            base.TakeDamage(physicalDamage, fireDamage, damageAnimation = "Damage_1");

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if (isBoss && enemyBossManager != null)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }

            enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);

            if (currentHealth <= 0)
            {
                HandleDeath();
                if (isBoss)
                {
                    worldEventManager.bossHasBeenDefeated = true;
                }
            }



        }

        private void HandleDeath()
        {
            currentHealth = 0;
            enemyAnimatorManager.PlayTargetAnimation("Death", true);
            isDead = true;
        }
    }
}

