using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState.StateData;
using UnityEngine;

namespace _Scripts.Enemies.EnemyTypes.Ronin
{
    public class EnemyRonin : Enemy
    {
        public EnemyRonin_IdleState IdleState { get; private set; }
        public EnemyRonin_MoveState MoveState { get; private set; }
        public EnemyRonin_PlayerDetectedState PlayerDetectedState { get; private set; }
        public EnemyRonin_ChargeState ChargeState { get; private set; }
        public EnemyRonin_LookForPlayerState LookForPlayerState { get; private set; }
        public EnemyRonin_MeleeAttackState MeleeAttackState { get; private set; }
        public EnemyRonin_StunState StunState { get; private set; }
        public EnemyRonin_DeadState DeadState { get; private set; }
        
        
        [SerializeField] private D_IdleState idleStateData;
        [SerializeField] private D_MoveState moveStateData;
        [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
        [SerializeField] private D_ChargeState chargeStateData;
        [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
        [SerializeField] private D_MeleeAttackState meleeAttackStateData;
        [SerializeField] private D_StunState stunStateData;

        [SerializeField] private Transform meleeAttackPosition;
        
        public override void Awake()
        {
            base.Awake();

            MoveState = new EnemyRonin_MoveState(this, StateMachine, "move", moveStateData, this);
            IdleState = new EnemyRonin_IdleState(this, StateMachine, "idle", idleStateData, this);
            PlayerDetectedState = new EnemyRonin_PlayerDetectedState(this, StateMachine, "playerDetected", playerDetectedStateData, this);
            ChargeState = new EnemyRonin_ChargeState(this, StateMachine, "charge", chargeStateData, this);
            LookForPlayerState = new EnemyRonin_LookForPlayerState(this, StateMachine, "lookForPlayer", lookForPlayerStateData, this);
            MeleeAttackState = new EnemyRonin_MeleeAttackState(this, StateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
            StunState = new EnemyRonin_StunState(this, StateMachine, "stun", stunStateData, this);
            DeadState = new EnemyRonin_DeadState(this, StateMachine, "dead", this);
            
            stats.Poise.OnCurrentValueZero += HandlePoiseZero;
        }

        private void Start() => StateMachine.Initialize(IdleState);

        private void HandlePoiseZero() => StateMachine.ChangeState(StunState);

        public void OnDestroy() => stats.Poise.OnCurrentValueZero -= HandlePoiseZero;

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
        }
    }
}
