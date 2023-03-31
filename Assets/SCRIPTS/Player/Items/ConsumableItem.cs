using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class ConsumableItem : Item
    {
        [Header("Item Quantity")]
        public int maxItemAmount;
        public int currentItemAmount;

        [Header("Item Model")]
        public GameObject itemModel;

        [Header("Animations")]
        public string consumableAnimation;
        public bool isInteracting;

        //To be deleted begins
        QuickSlotsUI quickSlotsUI;
        //To be deleted ends

        public virtual void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorHandler, PlayerWeaponSlotManager weaponSlotManager, PlayerFXManager playerFXManager)
        {
            if (playerAnimatorHandler.animator.GetBool("isInteracting") == false)
            {
                if (currentItemAmount > 0)
                {
                    playerAnimatorHandler.PlayTargetAnimation(consumableAnimation, isInteracting);
                    currentItemAmount -= 1;
                    quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
                    quickSlotsUI.itemCount = currentItemAmount;
                    quickSlotsUI.UpdateItemCount();
                    
                }
                else
                {
                    playerAnimatorHandler.PlayTargetAnimation("Flask_Empty", true);
                }
            }

            else
                return;
            
        }
    }
}

