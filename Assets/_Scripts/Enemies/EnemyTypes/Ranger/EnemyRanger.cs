using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;
using UnityEngine;

namespace _Scripts.Enemies.EnemyTypes.Ranger
{
    public class EnemyRanger : Enemy
    {
        public EnemyRanger_IdleState IdleState { get; private set; }
        public EnemyRanger_MoveState MoveState { get; private set; }
        public EnemyRanger_PlayerDetectedState PlayerDetectedState { get; private set; }
        public EnemyRanger_LookForPlayerState LookForPlayerState { get; private set; }
        public EnemyRanger_StunState StunState { get; private set; }
        public EnemyRanger_DeadState DeadState { get; private set; }
        public EnemyRanger_DodgeState DodgeState { get; private set; }
        public EnemyRanger_RangedAttackState RangedAttackState { get; private set; }
        
        
        [SerializeField] private D_IdleState idleStateData;
        [SerializeField] private D_MoveState moveStateData;
        [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
        [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
        [SerializeField] private D_StunState stunStateData;
        [SerializeField] public D_DodgeState dodgeStateData;
        [SerializeField] public D_RangedAttackState rangedAttackStateData;
        
        [SerializeField] private Transform rangedAttackPosition;
        
        public override void Awake()
        {
            base.Awake();

            MoveState = new EnemyRanger_MoveState(this, StateMachine, "move", moveStateData, this);
            IdleState = new EnemyRanger_IdleState(this, StateMachine, "idle", idleStateData, this);
            PlayerDetectedState = new EnemyRanger_PlayerDetectedState(this, StateMachine, "playerDetected", playerDetectedStateData, this);
            LookForPlayerState = new EnemyRanger_LookForPlayerState(this, StateMachine, "lookForPlayer", lookForPlayerStateData, this);
            StunState = new EnemyRanger_StunState(this, StateMachine, "stun", stunStateData, this);
            DeadState = new EnemyRanger_DeadState(this, StateMachine, "dead", this);
            DodgeState = new EnemyRanger_DodgeState(this, StateMachine, "dodge", dodgeStateData, this);
            RangedAttackState = new EnemyRanger_RangedAttackState(this, StateMachine, "shoot", rangedAttackPosition,
                rangedAttackStateData, this);
            
            stats.Poise.OnCurrentValueZero += HandlePoiseZero;
        }

        private void Start() => StateMachine.Initialize(IdleState);

        private void HandlePoiseZero() => StateMachine.ChangeState(StunState);

        public void OnDestroy() => stats.Poise.OnCurrentValueZero -= HandlePoiseZero;

        public void DestroyGameObject() => gameObject.SetActive(false);
        public void KillEnemy() => Invoke(nameof(DestroyGameObject), 1f);

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.DrawWireSphere(rangedAttackPosition.position, 0.01f);
        }
    }
}
