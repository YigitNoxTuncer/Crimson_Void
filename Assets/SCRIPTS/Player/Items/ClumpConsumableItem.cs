using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOX
{
    [CreateAssetMenu(menuName = "Items/Consumables/Cure Effect Clump")]

    public class ClumpConsumableItem : ConsumableItem
    {
        [Header("Recovery FX")]
        public GameObject clumpConsumeFX;

        [Header("Cure FX")]
        public bool curePoison;
        //cure Curse
        //cure Frost

        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManager weaponSlotManager, PlayerFXManager playerFXManager)
        {
            if (playerAnimatorManager.animator.GetBool("isInteracting") == false)
            {
                base.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManager, playerFXManager);
                GameObject clump = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
                playerFXManager.currentFX = clumpConsumeFX;
                playerFXManager.instantiatedFXModel = clump;
                if (curePoison)
                {
                    playerFXManager.poisonBuildUp = 0;
                    playerFXManager.poisonAmount = playerFXManager.defaultPoisonAmount;
                    playerFXManager.isPoisoned = false;

                    if(playerFXManager.currentPoisonParticleFX != null)
                    {
                        Destroy(playerFXManager.currentPoisonParticleFX);
                    }
                }
                weaponSlotManager.rightHandSlot.UnloadWeapon();
            }
            else
                return;
            }
        }
}

