using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOX
{
    public class IdleState : State
    {
        public PursueTargetState pursueTargetState;

        public LayerMask detectionLayer;

        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorHandler)
        {
            if (enemyManager.isInteracting)
                return this;

            #region Handle Enemy Target Detection
            Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, enemyManager.detectionRadius, detectionLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStatsManager characterStats = colliders[i].transform.GetComponent<CharacterStatsManager>();

                if (characterStats != null)
                {
                    if (characterStats.teamIDNumber != enemyStats.teamIDNumber)
                    {
                        Vector3 targetDirection = characterStats.transform.position - enemyManager.transform.position;
                        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

                        if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                        {
                            enemyManager.currentTarget = characterStats;
                        }
                    }
                }
            }
            #endregion

            #region Handle Switch To Next State
            if (enemyManager.currentTarget != null)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }
            #endregion
        }
    }
}

