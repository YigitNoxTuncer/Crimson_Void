using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOX
{
    public class IllusionaryWall : MonoBehaviour
    {
        public bool wallHasBeenHit;
        public Material illusionaryWallMaterial;
        public Material fadedIllusionaryWallMaterial;
        public float alpha;
        public float fadeTimer = 2.5f;
        public BoxCollider wallCollider;

        //public AudioSource audioSource;
        //public AudioClip illusionaryWallSound;

        private void Start()
        {
            fadedIllusionaryWallMaterial = Instantiate(illusionaryWallMaterial);
            gameObject.GetComponent<MeshRenderer>().material = fadedIllusionaryWallMaterial;
        }

        private void Update()
        {
            if (wallHasBeenHit)
            {
                FadeIllusionaryWall();
            }
        }

        public void FadeIllusionaryWall()
        {
            alpha = fadedIllusionaryWallMaterial.color.a;
            alpha -= Time.deltaTime / fadeTimer;
            Color fadedWallColor = new Color(1, 1, 1, alpha);
            fadedIllusionaryWallMaterial.color = fadedWallColor;

            if (wallCollider.enabled)
            {
                wallCollider.enabled = false;
                //audioSource.PlayOneShot(illusionaryWallSound);
            }

            if (alpha <= 0)
            {
                Destroy(this);
            }
        }
    }
}


