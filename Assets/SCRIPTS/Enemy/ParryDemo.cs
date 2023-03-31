using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class ParryDemo : MonoBehaviour
    {
        EnemyAnimatorManager enemyAnim;
        private void Start()
        {
            enemyAnim = GetComponentInChildren<EnemyAnimatorManager>();
        }

        void FixedUpdate()
        {
            StartCoroutine(ParryDemon());
        }

        IEnumerator ParryDemon()
        {
            yield return new WaitForSeconds(5);
            enemyAnim.animator.Play("OH_Light_Attack_1");
        }
    }

}
