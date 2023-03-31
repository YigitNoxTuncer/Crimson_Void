using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOX
{
    [CreateAssetMenu(menuName = "Items/Equipment/Chest")]
    public class ChestEquipment : EquipmentItem
    {
        public string chestModelName;
        public string upperRightArmModelName;
        public string upperLeftArmModelName;
        public string rightShoulderModelName;
        public string leftShoulderModelName;
        public string backModelName;
    }
}

