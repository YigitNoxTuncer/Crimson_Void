using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    [CreateAssetMenu(menuName = "Items/Consumables/Bomb Item")]

    public class BombConsumableItem : ConsumableItem
    {
        [Header("Velocity")]
        public int upwardVelocity = 50;
        public int forwardVelocity = 50;
        public int bombMass = 1;

        [Header("Live Bomb Model")]
        public GameObject liveBombModel;

        [Header("Base Damage")]
        public int baseDamage = 200;
        public int explosiveDamage = 75;

        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManager weaponSlotManager, PlayerFXManager playerFXManager)
        {
            if(currentItemAmount > 0)
            {
                base.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManager, playerFXManager);
                weaponSlotManager.rightHandSlot.UnloadWeapon();
                playerAnimatorManager.PlayTargetAnimation(consumableAnimation, true);
                GameObject bombModel = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform.position, Quaternion.identity, weaponSlotManager.rightHandSlot.transform);
                playerFXManager.instantiatedFXModel = bombModel;
            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation("Cant_spell", true);
            }
        }
    }
}

