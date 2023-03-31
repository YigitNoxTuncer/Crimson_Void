using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class SpellItem : Item
    {
        public GameObject spellWarmUpFX;
        public GameObject spellCastFX;
        public string spellAnimation;

        [Header("Spell Cost")]
        public int focusCost;

        [Header("Spell Type")]
        public bool isFaithSpell;
        public bool isMagicSpell;
        public bool isPyroSpell;

        [Header("Spell Description")]
        [TextArea]
        public string spellDescription;

        public virtual void AttemptToCastSpell(
            PlayerAnimatorManager animatorHandler, 
            PlayerStatsManager playerStats, 
            PlayerWeaponSlotManager weaponSlotManager)
        {
            Debug.Log("Attempted a cast!");
        }

        public virtual void SuccessfullyCastSpell(
            PlayerAnimatorManager animatorHandler, 
            PlayerStatsManager playerStats,
            CameraHandler cameraHandler,
            PlayerWeaponSlotManager weaponSlotManager)
        {
            Debug.Log("Casted a spell!");
            playerStats.DeductFocus(focusCost);
        }
    }
}

