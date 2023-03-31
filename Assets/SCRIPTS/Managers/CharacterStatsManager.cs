using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class CharacterStatsManager : MonoBehaviour
    {
        [Header("Team ID")]
        public int teamIDNumber = 0;

        public EnemyAnimatorManager animatorManager; // experimental

        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public int staminaLevel = 10;
        public float maxStamina;
        public float currentStamina;

        public int focusLevel = 10;
        public float maxFocus;
        public float currentFocus;

        public int soulCount = 0;
        public int soulsAwardedOnDeath;

        [Header("Poise")]
        public float totalPoiseDefense;
        public float offensivePoiseBonus; // hyperarmor
        public float armorPoiseBonus;
        public float totalPoiseResetTime = 5;
        public float poiseResetTimer = 0;


        [Header("Armor Absorptions")]
        public float physicalDamageAbsorptionHelm;
        public float physicalDamageAbsorptionChest;
        public float physicalDamageAbsorptionHands;
        public float physicalDamageAbsorptionLegs;

        public float fireDamageAbsorptionHelm;
        public float fireDamageAbsorptionChest;
        public float fireDamageAbsorptionHands;
        public float fireDamageAbsorptionLegs;

        /*
        public float magicalDamageAbsorptionHelm;
        public float magicalDamageAbsorptionChest;
        public float magicalDamageAbsorptionHands;
        public float magicalDamageAbsorptionLegs;
        */

        public bool isDead;
        public bool isBoss;

        protected virtual void Update()
        {
            HandlePoiseResetTimer();
        }

        private void Start()
        {
            totalPoiseDefense = armorPoiseBonus;
        }

        public virtual void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation = "Damage_1")
        {
            if (isDead)
                return;

            float totalPhysicalDamageAbsorption = 1 - (1 - physicalDamageAbsorptionHelm / 100) *
                                                      (1 - physicalDamageAbsorptionChest / 100) *
                                                      (1 - physicalDamageAbsorptionHands / 100) *
                                                      (1 - physicalDamageAbsorptionLegs / 100);

            physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

            Debug.Log("Total Physical Damage Absorption is " + totalPhysicalDamageAbsorption + "%");

            float totalFireDamageAbsorption = 1 - (1 - fireDamageAbsorptionHelm / 100) *
                                                  (1 - fireDamageAbsorptionChest / 100) *
                                                  (1 - fireDamageAbsorptionHands / 100) *
                                                  (1 - fireDamageAbsorptionLegs / 100);

            fireDamage = Mathf.RoundToInt(fireDamage - (fireDamage * totalFireDamageAbsorption));

            float finalDamage = physicalDamage + fireDamage; //+ magicalDamage

            currentHealth = Mathf.RoundToInt(currentHealth - finalDamage);

            Debug.Log("Total Damage Dealt is " + finalDamage);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public virtual void TakeDamageNoAnimation(int physicalDamage, int fireDamage)
        {
            if (isDead)
                return;

            float totalPhysicalDamageAbsorption = 1 - (1 - physicalDamageAbsorptionHelm / 100) *
                                                      (1 - physicalDamageAbsorptionChest / 100) *
                                                      (1 - physicalDamageAbsorptionHands / 100) *
                                                      (1 - physicalDamageAbsorptionLegs / 100);

            physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

            Debug.Log("Total Physical Damage Absorption is " + totalPhysicalDamageAbsorption + "%");

            float totalFireDamageAbsorption = 1 - (1 - fireDamageAbsorptionHelm / 100) *
                                                  (1 - fireDamageAbsorptionChest / 100) *
                                                  (1 - fireDamageAbsorptionHands / 100) *
                                                  (1 - fireDamageAbsorptionLegs / 100);

            fireDamage = Mathf.RoundToInt(fireDamage - (fireDamage * totalFireDamageAbsorption));

            float finalDamage = physicalDamage + fireDamage; //+ magicalDamage

            currentHealth = Mathf.RoundToInt(currentHealth - finalDamage);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public virtual void TakePoisonDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public void BossBreakGuard() //experimental
        {
            animatorManager.PlayTargetAnimation("Break_Guard", true);
        }

        public virtual void HandlePoiseResetTimer()
        {
            if(poiseResetTimer > 0)
            {
                poiseResetTimer = poiseResetTimer - Time.deltaTime;
            }
            else
            {
                totalPoiseDefense = armorPoiseBonus;
            }
        }
    }
}

