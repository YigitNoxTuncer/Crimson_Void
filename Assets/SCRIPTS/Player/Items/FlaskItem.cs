using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    [CreateAssetMenu(menuName = "Items/Consumables/Flask")]
    public class FlaskItem : ConsumableItem
    {
        [Header("Flask Type")]
        public bool crimsonFlask;
        public bool voidFlask;

        [Header("Recovery Amount")]
        public int healthRecoveryAmount;
        public int focusPointsRecoveryAmount;

        [Header("Recovery FX")]
        public GameObject recoveryFX;

        
        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorHandler, PlayerWeaponSlotManager weaponSlotManager, PlayerFXManager playerFXManager)
        {
            if (playerAnimatorHandler.animator.GetBool("isInteracting") == false)
            {
                base.AttemptToConsumeItem(playerAnimatorHandler, weaponSlotManager, playerFXManager);
                GameObject flask = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
                playerFXManager.currentFX = recoveryFX;
                playerFXManager.amountToBeHealed = healthRecoveryAmount;
                playerFXManager.instantiatedFXModel = flask;
                weaponSlotManager.rightHandSlot.UnloadWeapon();
            }
            else
                return;


            //recover hp/fp
            //insantiate flask in and and play drink anim
            //play recovery fx
        }
        
    }
}

