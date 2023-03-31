using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventoryManager playerInventoryManager;
        PlayerStatsManager playerStatsManager;

        [Header("Equipment Model Changers")]
        //Head
        HelmetModelChanger helmetModelChanger;
        //Chest
        ChestModelChanger chestModelChanger;
        UpperRightArmModelChanger upperRightArmModelChanger;
        UpperLeftArmModelChanger upperLeftArmModelChanger;
        RightShoulderModelChanger rightShoulderModelChanger;
        LeftShoulderModelChanger leftShoulderModelChanger;
        BackModelChanger backModelChanger;
        //Legs
        HipModelChanger hipModelChanger;
        RightLegModelChanger rightLegModelChanger;
        LeftLegModelChanger leftLegModelChanger;
        //Hands
        LowerRightArmModelChanger lowerRightArmModelChanger;
        LowerLeftArmModelChanger lowerLeftArmModelChanger;
        RightHandModelChanger rightHandModelChanger;
        LeftHandModelChanger leftHandModelChanger;
        RightElbowModelChanger rightElbowModelChanger;
        LeftElbowModelChanger leftElbowModelChanger;

        [Space(10), Header("Default Models")]

        [Header("Head")]
        //Head
        public GameObject nakedHeadModel;
        [Header("Chest")]
        //Chest
        public string nakedChestModel;
        public string nakedUpperRightArmModel;
        public string nakedUpperLeftArmModel;
        [Header("Legs")]
        //Legs
        public string nakedHipModel;
        public string nakedLeftLegModel;
        public string nakedRightLegModel;
        [Header("Hands")]
        //Hands
        public string nakedLowerRightArmModel;
        public string nakedLowerLeftArmModel;
        public string nakedRightHand;
        public string nakedLeftHand;
        [Space(10)]


        public BlockingCollider blockingCollider;

        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();

            //Head
            helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();
            //Chest
            chestModelChanger = GetComponentInChildren<ChestModelChanger>();
            upperRightArmModelChanger = GetComponentInChildren<UpperRightArmModelChanger>();
            upperLeftArmModelChanger = GetComponentInChildren<UpperLeftArmModelChanger>();
            rightShoulderModelChanger = GetComponentInChildren<RightShoulderModelChanger>();
            leftShoulderModelChanger = GetComponentInChildren<LeftShoulderModelChanger>();
            backModelChanger = GetComponentInChildren<BackModelChanger>();
            //Legs
            hipModelChanger = GetComponentInChildren<HipModelChanger>();
            rightLegModelChanger = GetComponentInChildren<RightLegModelChanger>();
            leftLegModelChanger = GetComponentInChildren<LeftLegModelChanger>();
            //Hands
            lowerRightArmModelChanger = GetComponentInChildren<LowerRightArmModelChanger>();
            lowerLeftArmModelChanger = GetComponentInChildren<LowerLeftArmModelChanger>();
            rightHandModelChanger = GetComponentInChildren<RightHandModelChanger>();
            leftHandModelChanger = GetComponentInChildren<LeftHandModelChanger>();
            rightElbowModelChanger = GetComponentInChildren<RightElbowModelChanger>();
            leftElbowModelChanger = GetComponentInChildren<LeftElbowModelChanger>();
        }

        private void Start()
        {
            EquipAllEquipmentModelsOnStart();
        }

        private void EquipAllEquipmentModelsOnStart()
        {
            //Helmet
            helmetModelChanger.UnequipAllHelmetModels();

            if (playerInventoryManager.currentHelmetEquipment != null)
            {
                nakedHeadModel.SetActive(false);
                helmetModelChanger.EquipHelmetModelByName(playerInventoryManager.currentHelmetEquipment.helmetModelName);
                playerStatsManager.physicalDamageAbsorptionHelm = playerInventoryManager.currentHelmetEquipment.phsyicalDefense;
                Debug.Log("Helm Absorption is " + playerStatsManager.physicalDamageAbsorptionHelm + "%");
            }
            else
            {
                nakedHeadModel.SetActive(true);
                playerStatsManager.physicalDamageAbsorptionHelm = 0;
            }

            //Chest
            chestModelChanger.UnequipAllChestModels();
            upperRightArmModelChanger.UnequipAllHandModels();
            upperLeftArmModelChanger.UnequipAllHandModels();
            rightShoulderModelChanger.UnequipAllShoulderModels();
            leftShoulderModelChanger.UnequipAllShoulderModels();
            backModelChanger.UnequipAllBackModels();

            if (playerInventoryManager.currentChestEquipment != null)
            {
                chestModelChanger.EquipChestModelByName(playerInventoryManager.currentChestEquipment.chestModelName);
                upperRightArmModelChanger.EquipHandModelByName(playerInventoryManager.currentChestEquipment.upperRightArmModelName);
                upperLeftArmModelChanger.EquipHandModelByName(playerInventoryManager.currentChestEquipment.upperLeftArmModelName);
                rightShoulderModelChanger.EquipShoulderModelByName(playerInventoryManager.currentChestEquipment.rightShoulderModelName);
                leftShoulderModelChanger.EquipShoulderModelByName(playerInventoryManager.currentChestEquipment.leftShoulderModelName);
                backModelChanger.EquipBackModelByName(playerInventoryManager.currentChestEquipment.backModelName);
                playerStatsManager.physicalDamageAbsorptionChest = playerInventoryManager.currentChestEquipment.phsyicalDefense;
                Debug.Log("Chest Absorption is " + playerStatsManager.physicalDamageAbsorptionChest + "%");
            }
            else
            {
                chestModelChanger.EquipChestModelByName(nakedChestModel);
                upperRightArmModelChanger.EquipHandModelByName(nakedUpperRightArmModel);
                upperLeftArmModelChanger.EquipHandModelByName(nakedUpperLeftArmModel);
                playerStatsManager.physicalDamageAbsorptionChest = 0;
            }

            //Legs

            hipModelChanger.UnequipAllHipModels();
            rightLegModelChanger.UnequipAllLegModels();
            leftLegModelChanger.UnequipAllLegModels();

            if(playerInventoryManager.currentLegsEquipment != null)
            {
                hipModelChanger.EquipHipModelByName(playerInventoryManager.currentLegsEquipment.hipModelName);
                rightLegModelChanger.EquipLegModelByName(playerInventoryManager.currentLegsEquipment.rightLegName);
                leftLegModelChanger.EquipLegModelByName(playerInventoryManager.currentLegsEquipment.leftLegName);
                playerStatsManager.physicalDamageAbsorptionLegs = playerInventoryManager.currentLegsEquipment.phsyicalDefense;
                Debug.Log("Legs Absorption is " + playerStatsManager.physicalDamageAbsorptionLegs + "%");
            }
            else
            {
                hipModelChanger.EquipHipModelByName(nakedHipModel);
                rightLegModelChanger.EquipLegModelByName(nakedRightLegModel);
                leftLegModelChanger.EquipLegModelByName(nakedLeftLegModel);
                playerStatsManager.physicalDamageAbsorptionLegs = 0;
            }

            //Hands

            lowerRightArmModelChanger.UnequipAllHandModels();
            lowerLeftArmModelChanger.UnequipAllHandModels();
            rightHandModelChanger.UnequipAllHandModels();
            leftHandModelChanger.UnequipAllHandModels();
            rightElbowModelChanger.UnequipAllElbowModels();
            leftElbowModelChanger.UnequipAllElbowModels();

            if (playerInventoryManager.currentHandsEquipment != null)
            {
                lowerRightArmModelChanger.EquipHandModelByName(playerInventoryManager.currentHandsEquipment.lowerRightArmModelName);
                lowerLeftArmModelChanger.EquipHandModelByName(playerInventoryManager.currentHandsEquipment.lowerLeftArmModelName);
                rightHandModelChanger.EquipHandModelByName(playerInventoryManager.currentHandsEquipment.rightHandModelName);
                leftHandModelChanger.EquipHandModelByName(playerInventoryManager.currentHandsEquipment.leftHandModelName);
                rightElbowModelChanger.EquipElbowModelByName(playerInventoryManager.currentHandsEquipment.rightElbowModelName);
                leftElbowModelChanger.EquipElbowModelByName(playerInventoryManager.currentHandsEquipment.leftElbowModelName);
                playerStatsManager.physicalDamageAbsorptionHands = playerInventoryManager.currentHandsEquipment.phsyicalDefense;
                Debug.Log("Hands Absorption is " + playerStatsManager.physicalDamageAbsorptionHands + "%");
            }
            else
            {
                lowerRightArmModelChanger.EquipHandModelByName(nakedLowerRightArmModel);
                lowerLeftArmModelChanger.EquipHandModelByName(nakedLowerLeftArmModel);
                rightHandModelChanger.EquipHandModelByName(nakedRightHand);
                leftHandModelChanger.EquipHandModelByName(nakedLeftHand);
                playerStatsManager.physicalDamageAbsorptionHands = 0;
            }


        }

        public void OpenBlockingCollider()
        {
            if (inputHandler.twoHandFlag)
            {
                blockingCollider.SetColliderDamageAbsorption(playerInventoryManager.rightWeapon);
            }
            else
            {
                blockingCollider.SetColliderDamageAbsorption(playerInventoryManager.leftWeapon);
            }

            blockingCollider.EnableBlockingCollider();

        }

        public void CloseBlockingCollider()
        {
            blockingCollider.DisableBlockingCollider();
        }
    }
}

