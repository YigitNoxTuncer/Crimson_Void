using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class CharacterFXManager : MonoBehaviour
    {
        CharacterStatsManager characterStatsManager;

        [Header("Damage FX")]
        public GameObject bloodSplatterFX;

        [Header("Weapon FX")]
        public WeaponFX rightWeaponFX;
        public WeaponFX leftWeaponFX;

        [Header("Poison")]
        public GameObject defaultPoisonParticleFX;
        public GameObject currentPoisonParticleFX;
        public Transform buildUpTransform;
        public bool isPoisoned;
        public float poisonBuildUp = 0;
        public float poisonAmount = 100;
        public float defaultPoisonAmount = 100;
        public float poisonTimer = 2;
        public int poisonDamage = 1;

        float timer;

        protected virtual void Awake()
        {
            characterStatsManager = GetComponent<CharacterStatsManager>();
        }

        public virtual void PlayWeaponFX(bool isLeft)
        {
            if (!isLeft)
            {
                if(rightWeaponFX != null)
                {
                    rightWeaponFX.PlayWeaponFX();
                }
            }
            else
            {
                if(leftWeaponFX != null)
                {
                    leftWeaponFX.PlayWeaponFX();
                }
            }
        }

        public virtual void StopWeaponFX(bool isLeft)
        {
            if (!isLeft)
            {
                if (rightWeaponFX != null)
                {
                    rightWeaponFX.StopWeaponFX();
                }
            }
            else
            {
                if (leftWeaponFX != null)
                {
                    leftWeaponFX.StopWeaponFX();
                }
            }
        }

        public virtual void PlayBloodSplatterFX(Vector3 bloodSplatterLocation)
        {
            GameObject blood = Instantiate(bloodSplatterFX, bloodSplatterLocation, Quaternion.identity);
        }

        public virtual void HandleAllBuildUpEffects()
        {
            if (characterStatsManager.isDead)
                return;

            HandlePoisonBuildUp();
            HandleIsPoisonedEffect();
        }

        protected virtual void HandlePoisonBuildUp()
        {
            if (isPoisoned)
                return;

            if (poisonBuildUp > 0 && poisonBuildUp < 100)
            {
                poisonBuildUp = poisonBuildUp - 1 * Time.deltaTime;
            }
            else if (poisonBuildUp >= 100)
            {
                isPoisoned = true;
                poisonBuildUp = 0;

                if(buildUpTransform != null)
                {
                    currentPoisonParticleFX = Instantiate(defaultPoisonParticleFX, buildUpTransform.transform);
                }
                else
                {
                    currentPoisonParticleFX = Instantiate(defaultPoisonParticleFX, characterStatsManager.transform);
                }
            }
        }

        protected virtual void HandleIsPoisonedEffect()
        {
            if (isPoisoned)
            {

                if(poisonAmount > 0)
                {
                    timer += Time.deltaTime;

                    if(timer >= poisonTimer)
                    {
                        characterStatsManager.TakePoisonDamage(poisonDamage);
                        timer = 0;
                    }

                    poisonAmount = poisonAmount - 1 * Time.deltaTime;
                }
                else
                {
                    isPoisoned = false;
                    poisonAmount = defaultPoisonAmount;
                    Destroy(currentPoisonParticleFX);
                }
            }
        }
    }
}

