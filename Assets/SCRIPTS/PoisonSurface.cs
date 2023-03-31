using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOX
{
    public class PoisonSurface : MonoBehaviour
    {
        public float poisonBuildUpAmount = 7;

        public List<CharacterFXManager> charactersInsidePoisonSurface;

        private void OnTriggerEnter(Collider other)
        {
            CharacterFXManager character = other.GetComponent<CharacterFXManager>();

            if (character != null)
            {
                charactersInsidePoisonSurface.Add(character);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CharacterFXManager character = other.GetComponent<CharacterFXManager>();

            if (character != null)
            {
                charactersInsidePoisonSurface.Remove(character);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            foreach (CharacterFXManager character in charactersInsidePoisonSurface)
            {
                if (character.isPoisoned)
                    return;

                character.poisonBuildUp = character.poisonBuildUp + poisonBuildUpAmount * Time.deltaTime;
            }
        }
    }
}
