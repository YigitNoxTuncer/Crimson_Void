using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    [CreateAssetMenu(menuName = "Items/Equipment/Hands")]
    public class HandsEquipment : EquipmentItem 
    {
        public string rightHandModelName;
        public string leftHandModelName;
        public string lowerRightArmModelName;
        public string lowerLeftArmModelName;
        public string rightElbowModelName;
        public string leftElbowModelName;
    }
}

