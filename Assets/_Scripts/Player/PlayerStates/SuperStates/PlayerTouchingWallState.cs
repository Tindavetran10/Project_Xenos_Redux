using _Scripts.Core.CoreComponents;
using _Scripts.Player.Data;
using _Scripts.Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace _Scripts.Player.PlayerStates.SuperStates
{
    public class PlayerTouchingWallState : PlayerState
    {
        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : Core.GetCoreComponent(ref _collisionSenses);

        private Movement _movement;
        private CollisionSenses _collisionSenses;

        #region Checks

        private bool _isGrounded;
        private bool _isTouchingWall;
        protected bool _isTouchingLedge;
        protected bool GrabInput;
        private bool _jumpInput;
        private int _xInput;
        protected int YInput;
        #endregion
        
        public PlayerTouchingWallState(PlayerFiniteStateMachine.Player player, PlayerStateMachine stateMachine, 
            PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {}


        public override void Enter() => base.Enter();

        public override void Exit() => base.Exit();

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _xInput = Player.InputHandler.NormInputX;
            YInput = Player.InputHandler.NormInputY;
            GrabInput = Player.InputHandler.GrabInput;
            _jumpInput = Player.InputHandler.JumpInput;

            if (_jumpInput)
            {
                Player.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
                StateMachine.ChangeState(Player.WallJumpState);
            }
            else if (_isGrounded && !GrabInput)
                StateMachine.ChangeState(Player.IdleState);
            else if (!_isTouchingWall || (_xInput != Movement?.FacingDirection && !GrabInput))
                StateMachine.ChangeState(Player.InAirState);
            else if (_isTouchingWall && !_isTouchingLedge)
                StateMachine.ChangeState(Player.LedgeClimbState);
        }

        public override void PhysicsUpdate() => base.PhysicsUpdate();

        protected override void DoChecks()
        {
            base.DoChecks();

            
            if (CollisionSenses)
            {
                _isGrounded = CollisionSenses.Ground;
                _isTouchingWall = CollisionSenses.WallFront;
                _isTouchingLedge = CollisionSenses.LedgeHorizontal;
            }
            
            if (_isTouchingWall && !_isTouchingLedge) {
                Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
            }
        }

        public override void AnimationTrigger() => base.AnimationTrigger();
        public override void AnimationFinishTrigger() => base.AnimationFinishTrigger();
    }
}
