using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOX
{
    public class PlayerFXManager : CharacterFXManager
    {
        PlayerStatsManager playerStatsManager;
        PlayerWeaponSlotManager playerWeaponSlotManager;

        PoisonBuildUpBar poisonBuildUpBar;
        PoisonAmountBar poisonAmountBar;

        public GameObject currentFX;
        public GameObject instantiatedFXModel;
        public int amountToBeHealed;

        protected override void Awake()
        {
            base.Awake();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();

            poisonBuildUpBar = FindObjectOfType<PoisonBuildUpBar>();
            poisonAmountBar = FindObjectOfType<PoisonAmountBar>();

            GeneralMethods.TODO("Add Fire Dot Here");
        }

        public void HealPlayerFromEffect()
        {
            playerStatsManager.HealPlayer(amountToBeHealed);
            GameObject healParticles = Instantiate(currentFX, playerStatsManager.transform);
        }

        public void DestroyInstantiatedFXModel()
        {
            Destroy(instantiatedFXModel.gameObject);
        }

        public void ReloadWeaponsOnBothHands()
        {
            playerWeaponSlotManager.LoadBothWeaponsOnSlots();
        }

        protected override void HandlePoisonBuildUp()
        {
            if(poisonBuildUp <= 0)
            {
                poisonBuildUpBar.gameObject.SetActive(false);
            }
            else
            {
                poisonBuildUpBar.gameObject.SetActive(true);
            }

            base.HandlePoisonBuildUp();
            poisonBuildUpBar.SetPoisonBuildUpAmount(Mathf.RoundToInt(poisonBuildUp));
        }

        protected override void HandleIsPoisonedEffect()
        {
            if (isPoisoned == false)
            {
                poisonAmountBar.gameObject.SetActive(false);
            }
            else
            {
                poisonAmountBar.gameObject.SetActive(true);
            }

            base.HandleIsPoisonedEffect();
            poisonAmountBar.SetPoisonAmount(Mathf.RoundToInt(poisonAmount));
        }
    }
}

