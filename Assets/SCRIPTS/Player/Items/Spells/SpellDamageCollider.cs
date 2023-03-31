using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class SpellDamageCollider : DamageCollider
    {
        public GameObject impactParticles;
        public GameObject projectileParticles;
        public GameObject muzzleParticles;

        bool hasCollided = false;

        CharacterStatsManager spellTarget;
        Rigidbody rigidBody;
        Vector3 impactNormal; //rotate particles

        protected override void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            projectileParticles = Instantiate(projectileParticles, transform.position, transform.rotation);
            projectileParticles.transform.parent = transform;

            if (muzzleParticles)
            {
                muzzleParticles = Instantiate(muzzleParticles, transform.position, transform.rotation);
                Destroy(muzzleParticles, 2f);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                return;
            }

            Debug.Log("EKUSUPUROJIAANN");
            if (!hasCollided)
            {
                spellTarget = collision.transform.GetComponent<CharacterStatsManager>();

                if (spellTarget != null && spellTarget.teamIDNumber != teamIDNumber)
                {
                    spellTarget.TakeDamage(0, fireDamage);
                }


                hasCollided = true;
                impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

                Destroy(projectileParticles);
                Destroy(impactParticles, 2f);
                Destroy(gameObject, 2f);
            }
        }
    }
}

