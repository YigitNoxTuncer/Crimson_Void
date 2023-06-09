using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace NOX
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimationManager;
        EnemyStatsManager enemyStatsManager;
        EnemyFXManager enemyFXManager;

        public State currentState;
        public CharacterStatsManager currentTarget;
        public NavMeshAgent navMeshAgent;
        public Rigidbody enemyRigidBody;

        public bool isPerformingAction;
        public float rotationSpeed = 40;
        public float maximumAggroRadius = 1.5f;

        [Header("A.I Settings")]
        public float detectionRadius = 20;
        //Enemy FOV
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;

        public float currentRecoveryTime = 0;

        [Header("A.I Combat Settings")]
        public bool allowAIToPerformCombos;
        public bool isPhaseShifting;
        public float comboLikelyHood;

        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAnimationManager = GetComponent<EnemyAnimatorManager>();
            enemyFXManager = GetComponent<EnemyFXManager>();
            enemyRigidBody = GetComponent<Rigidbody>();
            enemyStatsManager = GetComponent<EnemyStatsManager>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            //navMeshAgent.enabled = false;
        }

        private void Start()
        {
            navMeshAgent.enabled = false;
            enemyRigidBody.isKinematic = false;
        }

        private void Update()
        {
            HandleRecoveryTimer();
            HandleStateMachine();

            isRotatingWithRootMotion = enemyAnimationManager.animator.GetBool("isRotatingWithRootMotion");
            isInteracting = enemyAnimationManager.animator.GetBool("isInteracting");
            isPhaseShifting = enemyAnimationManager.animator.GetBool("isPhaseShifting");
            isInvulnerable = enemyAnimationManager.animator.GetBool("isInvulnerable");
            canDoCombo = enemyAnimationManager.animator.GetBool("canDoCombo");
            canRotate = enemyAnimationManager.animator.GetBool("canRotate");
            enemyAnimationManager.animator.SetBool("isDead", enemyStatsManager.isDead);
        }

        private void FixedUpdate()
        {
            enemyFXManager.HandleAllBuildUpEffects();
        }

        private void LateUpdate()
        {
            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;
        }

        private void HandleStateMachine()
        {
            if (currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStatsManager, enemyAnimationManager);

                if(nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        private void HandleRecoveryTimer()
        {
            if(currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPerformingAction)
            {
                if(currentRecoveryTime <= 0)
                {
                    isPerformingAction = false;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}

