﻿using _Scripts.Player.Data;
using _Scripts.Player.PlayerFiniteStateMachine;
using _Scripts.Player.PlayerStates.SuperStates;

namespace _Scripts.Player.PlayerStates.SubStates
{
    public class PlayerCrouchIdleState : PlayerGroundedState
    {
        public PlayerCrouchIdleState(PlayerFiniteStateMachine.Player player, PlayerStateMachine stateMachine, 
            PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

        public override void Enter()
        {
            base.Enter();
            Movement?.SetVelocityZero();
            Player.SetColliderHeight(PlayerData.crouchColliderHeight);
        }

        public override void Exit()
        {
            base.Exit();
            Player.SetColliderHeight(PlayerData.standColliderHeight);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!IsExitingState)
            {
                if (XInput != 0)
                    StateMachine.ChangeState(Player.CrouchMoveState);
                else if (YInput != -1 && !IsTouchingCeiling) 
                    StateMachine.ChangeState(Player.IdleState);
            }
        }
    }

}