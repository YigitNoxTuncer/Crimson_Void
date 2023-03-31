using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class PlayerWeaponSlotManager : CharacterWeaponSlotManager
    {
        QuickSlotsUI quickSlotsUI;

        Animator animator;
        InputHandler inputHandler;
        PlayerManager playerManager;
        public PlayerInventoryManager playerInventoryManager;
        PlayerStatsManager playerStatsManager;
        PlayerFXManager playerFXManager;
        CameraHandler cameraHandler;

        [Header("Attacking Weapon")]
        public WeaponItem attackingWeapon;

        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
            inputHandler = GetComponent<InputHandler>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerManager = GetComponent<PlayerManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerFXManager = GetComponent<PlayerFXManager>();
            animator = GetComponent<Animator>();
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();

            LoadWeaponHolderSlots();


        }

        //TO BE DELETED BEGIN
        private void Start()
        {
            quickSlotsUI.UpdateConsumableQuickSlotsUI(playerInventoryManager.currentConsumableItem);
            quickSlotsUI.UpdateSpellQuickSlotsUI(playerInventoryManager.currentSpell);
            playerInventoryManager.currentConsumableItem.currentItemAmount = playerInventoryManager.currentConsumableItem.maxItemAmount;
        }
        //TO BE DELETED END

        private void LoadWeaponHolderSlots()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
                else if (weaponSlot.isBackSlot)
                {
                    backSlot = weaponSlot;
                }
            }
        }

        public void LoadBothWeaponsOnSlots()
        {
            LoadWeaponOnSlot(playerInventoryManager.rightWeapon, false);
            LoadWeaponOnSlot(playerInventoryManager.leftWeapon, true);
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if(weaponItem != null)
            {
                if (isLeft)
                {
                    leftHandSlot.currentWeapon = weaponItem;
                    leftHandSlot.LoadWeaponModel(weaponItem);
                    LoadLeftWeaponDamageCollider();
                    quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);
                    animator.CrossFade(weaponItem.left_hand_idle, 0.2f);
                }
                else
                {
                    if (inputHandler.twoHandFlag)
                    {
                        backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                        leftHandSlot.UnloadWeaponAndDestroy();
                        animator.CrossFade(weaponItem.th_idle, 0.2f);
                    }
                    else
                    {
                        animator.CrossFade("Both Arms Empty", 0.2f);
                        backSlot.UnloadWeaponAndDestroy();
                        animator.CrossFade(weaponItem.right_hand_idle, 0.2f);
                    }

                    rightHandSlot.currentWeapon = weaponItem;
                    rightHandSlot.LoadWeaponModel(weaponItem);
                    LoadRightWeaponDamageCollider();
                    quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
                }
            }
            else
            {
                weaponItem = unarmedWeapon;

                if (isLeft)
                {
                    animator.CrossFade("Left Arm Empty", 0.2f);
                    playerInventoryManager.leftWeapon = unarmedWeapon;
                    leftHandSlot.currentWeapon = unarmedWeapon;
                    leftHandSlot.LoadWeaponModel(unarmedWeapon);
                    LoadLeftWeaponDamageCollider();
                    quickSlotsUI.UpdateWeaponQuickSlotsUI(true, unarmedWeapon);
                }
                else
                {
                    animator.CrossFade("Right Arm Empty", 0.2f);
                    playerInventoryManager.rightWeapon = unarmedWeapon;
                    rightHandSlot.currentWeapon = unarmedWeapon;
                    rightHandSlot.LoadWeaponModel(unarmedWeapon);
                    LoadRightWeaponDamageCollider();
                    quickSlotsUI.UpdateWeaponQuickSlotsUI(false, unarmedWeapon);
                }
            }
        }

        public void SuccessfullyThrowFireBomb()
        {
            Destroy(playerFXManager.instantiatedFXModel);
            BombConsumableItem fireBombItem = playerInventoryManager.currentConsumableItem as BombConsumableItem;

            GameObject activeModelBomb = Instantiate(fireBombItem.liveBombModel, rightHandSlot.transform.position + /*experimental*/new Vector3(-1,0,-0.5f)/*to center bomb*/, cameraHandler.cameraPivotTransform.rotation);
            activeModelBomb.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTransform.eulerAngles.x, playerManager.lockOnTransform.eulerAngles.y, 0);
            BombDamageCollider damageCollider = activeModelBomb.GetComponentInChildren<BombDamageCollider>();

            damageCollider.explosionDamage = fireBombItem.baseDamage;
            damageCollider.explosionSplashDamage = fireBombItem.explosiveDamage;
            damageCollider.bombRigidBody.AddForce(activeModelBomb.transform.forward * fireBombItem.forwardVelocity);
            damageCollider.bombRigidBody.AddForce(activeModelBomb.transform.up * fireBombItem.upwardVelocity);
            damageCollider.teamIDNumber = playerStatsManager.teamIDNumber;
            LoadWeaponOnSlot(playerInventoryManager.rightWeapon, false);

        }

        #region Handle Weapon's Damage Collider

        private void LoadLeftWeaponDamageCollider()
        {
            if (rightHandSlot.currentWeapon.isMeleeWeapon)
            {
                leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

                //leftHandDamageCollider.teamIDNumber = playerStatsManager.teamIDNumber;
                //leftHandDamageCollider.currentWeaponDamage = playerInventoryManager.leftWeapon.baseDamage; //check if left weapon is empty or not
                //leftHandDamageCollider.poiseBreak = playerInventoryManager.leftWeapon.poiseBreak;
                //playerFXManager.leftWeaponFX = leftHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();

            }
            else
                return;
        }

        private void LoadRightWeaponDamageCollider()
        {
            if (rightHandSlot.currentWeapon.isMeleeWeapon)
            {
                rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

                rightHandDamageCollider.physicalDamage = playerInventoryManager.rightWeapon.physicalDamage;
                rightHandDamageCollider.fireDamage = playerInventoryManager.rightWeapon.fireDamage;

                rightHandDamageCollider.teamIDNumber = playerStatsManager.teamIDNumber;

                rightHandDamageCollider.poiseBreak = playerInventoryManager.rightWeapon.poiseBreak;
                playerFXManager.rightWeaponFX = rightHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
            }
            else
                return;
        }

        public void OpenDamageCollider()
        {
            if (playerManager.isUsingRightHand)
            {
                rightHandDamageCollider.EnableDamageCollider();
                playerFXManager.PlayWeaponFX(false);
            }
            else if (playerManager.isUsingLeftHand)
            {
                leftHandDamageCollider.EnableDamageCollider();
            }
        }
        public void CloseDamageCollider()
        {
            if (rightHandDamageCollider != null)
            {
                rightHandDamageCollider.DisableDamageCollider();
                playerFXManager.StopWeaponFX(false);
            }
            
            if (leftHandDamageCollider != null)
            {
                leftHandDamageCollider.DisableDamageCollider();
            }
        }

        #endregion

        #region Handle Weapon's Stamina Drainage

        public void DrainStaminaLightAttack()
        {
            playerStatsManager.TakeStaminaDamage(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier);
        }

        public void DrainStaminaHeavyAttack()
        {
            playerStatsManager.TakeStaminaDamage(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier);
        }

        #endregion

        #region Handle Weapon's Poise Bonus

        public void GrantWeaponAttackingPoiseBonus()
        {
            playerStatsManager.totalPoiseDefense = playerStatsManager.totalPoiseDefense + attackingWeapon.offensivePoiseBonus;
        }

        public void ResetWeaponAttackingPoiseBonus()
        {
            playerStatsManager.totalPoiseDefense = playerStatsManager.armorPoiseBonus;
        }

        #endregion
    }

}

