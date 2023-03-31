using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOX
{
    public class WeaponFX : MonoBehaviour
    {
        [Header("Weapon FX")]
        public ParticleSystem normalWeaponTrail1;
        public ParticleSystem normalWeaponTrail2;
        public ParticleSystem normalWEaponTrail3;
        //elemental weapon trails

        private void Start()
        {
            StopWeaponFX();
        }

        public void PlayWeaponFX()
        {
            normalWeaponTrail1.Play();
            normalWeaponTrail2.Play();
            normalWEaponTrail3.Play();
        }

        public void StopWeaponFX()
        {
            normalWeaponTrail1.Stop();
            normalWeaponTrail2.Stop();
            normalWEaponTrail3.Stop();
        }
    }
}
