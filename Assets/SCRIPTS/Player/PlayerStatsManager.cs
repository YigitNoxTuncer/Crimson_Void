using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOX
{
    public class PlayerStatsManager : CharacterStatsManager
    {
        PlayerManager playerManager;
        [SerializeField]HealthBar healthBar;
        StaminaBar staminaBar;
        FocusBar focusBar;
        InputHandler inputHandler;
        PlayerDeathManager playerDeathManager;

        PlayerAnimatorManager playerAnimatorManager;

        public float staminaRegenarationAmount = 30;
        float staminaRegenTimer = 0;

        public float rollStaminaCost = 15; //WIP
        public float sprintStaminaCost = 2; //WIP

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            playerDeathManager = GetComponent<PlayerDeathManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            inputHandler = GetComponent<InputHandler>();

            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            focusBar = FindObjectOfType<FocusBar>();
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrentHealth(currentHealth);

            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
            staminaBar.SetCurrentStamina(currentStamina);

            maxFocus = SetMaxFocusFromFocusLevel();
            currentFocus = maxFocus;
            focusBar.SetMaxFocus(maxFocus);
            focusBar.SetCurrentFocus(currentFocus);

            playerDeathManager.GameOverScreenDisabled();
        }

        public override void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer = poiseResetTimer - Time.deltaTime;
            }
            else if (poiseResetTimer <= 0 && !playerManager.isInteracting)
            {
                totalPoiseDefense = armorPoiseBonus;
            }
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        private float SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        private float SetMaxFocusFromFocusLevel()
        {
            maxFocus = focusLevel * 10;
            return maxFocus;
        }

        public override void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation = "Damage_1")
        {
            if (playerManager.isInvulnerable)
                return;

            base.TakeDamage(physicalDamage, fireDamage, damageAnimation = "Damage_1");

            healthBar.SetCurrentHealth(currentHealth);

            playerAnimatorManager.PlayTargetAnimation(damageAnimation, true);

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                playerAnimatorManager.PlayTargetAnimation("Death", true);
                isDead = true;
                inputHandler.enabled = false;
                playerDeathManager.GameOverScreenEnabled();
            }
        }

        public override void TakePoisonDamage(int damage)
        {
            if (isDead)
                return;

            base.TakePoisonDamage(damage);
            healthBar.SetCurrentHealth(currentHealth);


            if (currentHealth <= 0)
            {
                currentHealth = 0;
                playerAnimatorManager.PlayTargetAnimation("Death", true);
                isDead = true;
                inputHandler.enabled = false;
                playerDeathManager.GameOverScreenEnabled();
            }
        }

        public override void TakeDamageNoAnimation(int physicalDamage, int fireDamage)
        {
            base.TakeDamageNoAnimation(physicalDamage, fireDamage);
            healthBar.SetCurrentHealth(currentHealth);

            if (isDead)
            {
                playerDeathManager.GameOverScreenEnabled();
            }

        }

        public void TakeStaminaDamage(float damage)
        {
            currentStamina -= damage;

            staminaBar.SetCurrentStamina(currentStamina);
        }

        public void RegenarateStamina()
        {
            if (playerManager.isInteracting)
            {
                staminaRegenTimer = 0;
            }
            else if (inputHandler.sprintFlag)
            {
                staminaRegenTimer = 0;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;

                if (currentStamina < maxStamina && staminaRegenTimer > 0.5f)
                {
                    currentStamina += staminaRegenarationAmount * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }
        }

        public void HealPlayer(int healAmount)
        {
            currentHealth += healAmount;

            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            healthBar.SetCurrentHealth(currentHealth);
        }

        public void DeductFocus(int focus)
        {
            currentFocus -= focus;
            
            if(currentFocus < 0)
            {
                currentFocus = 0;
            }
             
            focusBar.SetCurrentFocus(currentFocus);
        }

        public void AddSouls(int souls)
        {
            soulCount += souls;
        }
    }
}

