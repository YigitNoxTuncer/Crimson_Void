using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_Input;
        public bool a_Input;
        public bool x_Input;
        public bool y_Input;
        public bool ctrl_Input;
        public bool rb_Input;
        public bool lb_Input;
        public bool rt_Input;
        public bool lt_Input;
        public bool critical_Attack_Input;
        public bool jump_Input;
        public bool inventory_Input;
        public bool lockOn_Input;
        public bool right_Stick_Right_Input;
        public bool right_Stick_Left_Input;


        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Right;
        public bool d_Pad_Left;


        public bool rollFlag;
        public bool sprintFlag;
        public bool walkFlag;
        public bool twoHandFlag;
        public bool comboFlag;
        public bool lockOnFlag;
        public bool inventoryFlag;
        public float rollInputTimer;
        public float walkInputTimer;

        public Transform criticalAttackRaycastStartPoint;


        PlayerControls inputActions;
        PlayerCombatManager playerCombatManager;
        PlayerInventoryManager playerInventoryManager;
        PlayerManager playerManager;
        PlayerAnimatorManager playerAnimatorManager;
        PlayerFXManager playerFXManager;
        PlayerStatsManager playerStatsManager;
        BlockingCollider blockingCollider;
        PlayerWeaponSlotManager weaponSlotManager;
        CameraHandler cameraHandler;
        UIManager uiManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerCombatManager = GetComponent<PlayerCombatManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerManager = GetComponent<PlayerManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerFXManager = GetComponent<PlayerFXManager>();
            weaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
            blockingCollider = GetComponentInChildren<BlockingCollider>();
            uiManager = FindObjectOfType<UIManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        }


        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>(); //Movement Input
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>(); // Camera Input
                inputActions.PlayerActions.RB.performed += i => rb_Input = true; // Right Bumper Input
                inputActions.PlayerActions.RT.performed += i => rt_Input = true; // Right Trigger Input
                inputActions.PlayerActions.LT.performed += i => lt_Input = true; // Left Trigger Input
                inputActions.PlayerActions.LB.performed += i => lb_Input = true; // Left Bumper Input
                inputActions.PlayerActions.LB.canceled += i => lb_Input = false; // Left Bumper Input
                inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true; // D Pad Right Input
                inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true; // D Pad Left Input
                inputActions.PlayerActions.A.performed += i => a_Input = true; // A Input
                inputActions.PlayerActions.X.performed += i => x_Input = true; // X Input
                inputActions.PlayerActions.Roll.performed += i => b_Input = true; // B Input
                inputActions.PlayerActions.Roll.canceled += i => b_Input = false; // B Input
                inputActions.PlayerActions.Jump.performed += i => jump_Input = true; // Jump Input
                inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true; // Inventory Input
                inputActions.PlayerActions.LockOn.performed += i => lockOn_Input = true; // Lock On Input
                inputActions.PlayerMovement.LockOnTargetRight.performed += i => right_Stick_Right_Input = true; // Right Stick Right Movement for Lock On
                inputActions.PlayerMovement.LockOnTargetLeft.performed += i => right_Stick_Left_Input = true; // Right Stick Left Movement for Lock On
                inputActions.PlayerActions.Y.performed += i => y_Input = true; // Y/Triangle input
                inputActions.PlayerActions.CriticalAttack.performed += i => critical_Attack_Input = true; // Hold RB for critical
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            HandleMoveInput(delta);
            HandleRollInput(delta);
            WalkInput(delta);
            HandleCombatInput(delta);
            HandleQuickSlotsInput();
            HandleInventoryInput();
            HandleLockOnInput();
            HandleTwoHandInput();
            HandleCriticalAttackInput();
            HandleUseConsumableInput();
        }

        private void HandleMoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {
            /*
            if (playerStats.currentStamina < 0) // WIP
            {
                sprintFlag = false;
                rollFlag = false;
                return;
            }
            */

            if (b_Input)
            {
                rollInputTimer += delta;

                if(playerStatsManager.currentStamina <= 0)
                {
                    b_Input = false;
                    sprintFlag = false;
                }

                if(moveAmount > 0.5f && playerStatsManager.currentStamina > 0)
                {
                    sprintFlag = true;
                }
            }
            else
            {
                sprintFlag = false;

                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }

        }

        private void WalkInput(float delta)
        {
            ctrl_Input = inputActions.PlayerActions.Walk.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            walkFlag = ctrl_Input;

            if (ctrl_Input)
            {
                walkInputTimer += delta;
            }
            else
            {
                walkFlag = false;
                walkInputTimer = 0;
            }
        }

        private void HandleCombatInput(float delta)
        {
            //RB = Right Bumper
            //RT = Right Trigger
            /*
            if (playerStats.currentStamina < 0) // WIP
                return;
            */

            if (rb_Input)
            {
                playerCombatManager.HandleRBAction();
            }

            if (rt_Input)
            {
                if (playerManager.isInteracting)
                    return;

                if (playerManager.canDoCombo)
                    return;

                playerAnimatorManager.animator.SetBool("isUsingRightHand", true);
                playerCombatManager.HandleHeavyAttack(playerInventoryManager.rightWeapon);
            }

            if (lb_Input)
            {
                playerCombatManager.HandleLBAction();
            }
            else
            {
                playerManager.isBlocking = false;

                if (blockingCollider.blockingCollider.enabled)
                {
                    blockingCollider.DisableBlockingCollider();
                }
            }

            if (lt_Input)
            {
                if (twoHandFlag)
                {
                    //if two handing handle weapon art
                }
                else
                {
                    playerCombatManager.HandleLTAction();
                }
            }
        }

        private void HandleQuickSlotsInput()
        {

            if (d_Pad_Right)
            {
                playerInventoryManager.ChangeRightWeapon();
            }
            else if (d_Pad_Left)
            {
                playerInventoryManager.ChangeLeftWeapon();
            }

        }

        private void HandleInventoryInput()
        {

            if (inventory_Input)
            {
                inventoryFlag = !inventoryFlag;

                if (inventoryFlag)
                {
                    uiManager.OpenSelectWindow();
                    uiManager.UpdateUI();
                    uiManager.hudWindow.SetActive(false);
                }

                else
                {
                    uiManager.CloseSelectWindow();
                    uiManager.CloseAllInventoryWindows();
                    uiManager.hudWindow.SetActive(true);
                }
            }
        }

        private void HandleLockOnInput()
        {
            if (lockOn_Input && !lockOnFlag)
            {
                lockOn_Input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true;
                }
            }
            else if (lockOn_Input && lockOnFlag)
            {
                lockOn_Input = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();
            }

            if (lockOnFlag && right_Stick_Left_Input)
            {
                right_Stick_Left_Input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.leftLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
                }
            }

            else if (lockOnFlag && right_Stick_Right_Input)
            {
                right_Stick_Right_Input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.rightLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
                }
            }

        }

        private void HandleTwoHandInput()
        {
            if (y_Input)
            {
                y_Input = false;

                twoHandFlag = !twoHandFlag;

                if (twoHandFlag)
                {
                    weaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.rightWeapon, false);
                }
                if (!twoHandFlag)
                {
                    weaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.rightWeapon, false);
                    weaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.leftWeapon, true);
                }
            }
        }

        private void HandleCriticalAttackInput()
        {
            if (critical_Attack_Input)
            {
                critical_Attack_Input = false;
                playerCombatManager.AttemptBackStabOrRiposte();
            }
        }

        private void HandleUseConsumableInput()
        {
            if (x_Input)
            {
                x_Input = false;
                playerInventoryManager.currentConsumableItem.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManager, playerFXManager);
            }
        }

    }







}
