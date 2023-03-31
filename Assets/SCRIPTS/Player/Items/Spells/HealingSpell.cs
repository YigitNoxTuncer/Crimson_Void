using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{   
    [CreateAssetMenu(menuName = "Spells/Healing Spell")]
    public class HealingSpell : SpellItem
    {
        public int healAmount;

        public override void AttemptToCastSpell(
            PlayerAnimatorManager animatorHandler, 
            PlayerStatsManager playerStats,
            PlayerWeaponSlotManager weaponSlotManager)
        {
            base.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);
            GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, animatorHandler.transform);
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
            Debug.Log("Attempt to cast");
        }

        public override void SuccessfullyCastSpell(
            PlayerAnimatorManager animatorHandler, 
            PlayerStatsManager playerStats,
            CameraHandler cameraHandler,
            PlayerWeaponSlotManager weaponSlotManager)
        {
            base.SuccessfullyCastSpell(animatorHandler, playerStats, cameraHandler, weaponSlotManager);
            GameObject instantiatedSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
            playerStats.HealPlayer(healAmount);
            Debug.Log("Success!");
        }
    }
}

