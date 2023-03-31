using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class CharacterManager : MonoBehaviour
    {
        [Header("Lock On Transform")]
        public Transform lockOnTransform;

        [Header("Combat Colliders")]
        public CriticalDamageCollider backstabCollider;
        public CriticalDamageCollider riposteCollider;

        [Header("Interaction")]
        public bool isInteracting;

        [Header("Combat Flags")]
        public bool canBeRiposted;
        public bool canBeParried;
        public bool canDoCombo;
        public bool isParrying;
        public bool isBlocking;
        public bool isInvulnerable;
        public bool isUsingRightHand;
        public bool isUsingLeftHand;

        [Header("Movement Flags")]
        public bool isRotatingWithRootMotion;
        public bool canRotate;

        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;


        [Header("Spell Flags")]
        public bool isFiringSpell;

        //Damage on anim event
        //for backstab and riposte
        public int pendingCriticalDamage;

        public bool isPlayer;


    }
}

