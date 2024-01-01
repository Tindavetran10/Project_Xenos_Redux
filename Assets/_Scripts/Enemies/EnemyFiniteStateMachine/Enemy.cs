using _Scripts.CoreSystem;
using _Scripts.CoreSystem.CoreComponents;
using _Scripts.Enemies.Data;
using _Scripts.Intermediaries;
using UnityEngine;

namespace _Scripts.Enemies.EnemyFiniteStateMachine
{
    // This script gonna be a base for all types of Enemy
    public class Enemy : MonoBehaviour
    {
        #region Components
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        

        protected EnemyStateMachine StateMachine;
        public EnemyData enemyData;
        public Core Core { get; private set; }
        public Animator Anim { get; private set; }
        public AnimationToStateMachine Atsm { get; private set; }
        public int LastDamageDirection { get; private set; }
        #endregion
        
        
        [SerializeField] private Transform playerCheck;
        
        
        private float _currentHealth;
        private float _currentStunResistance;
        private float _lastDamageTime;

        private Vector2 _velocityWorkspace;

        protected bool isStunned;
        protected bool isDead;

        protected Stats stats;
        
        private static readonly int YVelocity = Animator.StringToHash("yVelocity");

        public virtual void Awake() {
            Core = GetComponentInChildren<Core>();

            stats = Core.GetCoreComponent<Stats>();
            
            _currentHealth = enemyData.maxHealth;
            _currentStunResistance = enemyData.stunResistance;

            Anim = GetComponent<Animator>();
            Atsm = GetComponent<AnimationToStateMachine>();

            StateMachine = new EnemyStateMachine();
        }
        
        public virtual void Update() {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();

            Anim.SetFloat(YVelocity, Movement.Rb.velocity.y);

            if (Time.time >= _lastDamageTime + enemyData.stunRecoveryTime) 
                ResetStunResistance();
        }
        
        public virtual void ResetStunResistance() {
            isStunned = false;
            _currentStunResistance = enemyData.stunResistance;
        }
        
        public virtual void FixedUpdate() => StateMachine.CurrentState.PhysicsUpdate();
        
        public virtual bool CheckPlayerInMinAgroRange() => 
            Physics2D.Raycast(playerCheck.position, transform.right, enemyData.minAgroDistance, enemyData.whatIsPlayer);

        public virtual bool CheckPlayerInMaxAgroRange() => 
            Physics2D.Raycast(playerCheck.position, transform.right, enemyData.maxAgroDistance, enemyData.whatIsPlayer);

        public virtual bool CheckPlayerInCloseRangeAction() => 
            Physics2D.Raycast(playerCheck.position, transform.right, enemyData.closeRangeActionDistance, enemyData.whatIsPlayer);
        
        
        public virtual void OnDrawGizmos() {
            if (Core != null) {
                var playerCheckPosition = playerCheck.position;
                
                Gizmos.DrawWireSphere(playerCheckPosition + (Vector3)(Vector2.right * enemyData.closeRangeActionDistance), 0.2f);
                Gizmos.DrawWireSphere(playerCheckPosition + (Vector3)(Vector2.right * enemyData.minAgroDistance), 0.2f);
                Gizmos.DrawWireSphere(playerCheckPosition + (Vector3)(Vector2.right * enemyData.maxAgroDistance), 0.2f);
            }
        }
    }
}
