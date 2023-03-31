using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace NOX
{
    public class QuickSlotsUI : MonoBehaviour
    {
        public Image leftWeaponIcon;
        public Image rightWeaponIcon;
        public Image consumableIcon;
        public Image spellIcon;

        public int itemCount = 0;
        public Text itemCountText;

        public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weapon)
        {
            if(isLeft == false)
            {
                if(weapon.itemIcon != null)
                {
                    rightWeaponIcon.sprite = weapon.itemIcon;
                    rightWeaponIcon.enabled = true;
                }
                else
                {
                    rightWeaponIcon.sprite = null;
                    rightWeaponIcon.enabled = false;
                }
            }
            else
            {
                if(weapon.itemIcon != null)
                {
                    leftWeaponIcon.sprite = weapon.itemIcon;
                    leftWeaponIcon.enabled = true;
                }
                else
                {
                    leftWeaponIcon.sprite = null;
                    leftWeaponIcon.enabled = false;
                }
            }
        }

        public void UpdateConsumableQuickSlotsUI(ConsumableItem consumable)
        {
            if(consumable.itemIcon != null)
            {
                consumableIcon.sprite = consumable.itemIcon;
                consumableIcon.enabled = true;
            }
            else
            {
                consumableIcon.sprite = null;
                consumableIcon.enabled = false;
            }
        }
        //to be deleted begins
        private void Start()
        {
            itemCountText.text = "5";
        }
        //to be deleted ends
        public void UpdateItemCount()
        {
            itemCountText.text = itemCount.ToString();
        }

        public void UpdateSpellQuickSlotsUI(SpellItem spell)
        {
            if(spell.itemIcon != null)
            {
                spellIcon.sprite = spell.itemIcon;
                spellIcon.enabled = true;
            }
            else
            {
                spellIcon.sprite = null;
                spellIcon.enabled = false;
            }
        }
    }
}

