﻿using _Scripts.Player.Data;
using UnityEngine;

namespace _Scripts.Player.PlayerFiniteStateMachine
{
     public class PlayerState
     {
          protected readonly CoreSystem.Core Core;

          protected readonly Player Player;
          protected readonly PlayerStateMachine StateMachine;
          protected readonly PlayerData PlayerData;
     
          protected bool IsAnimationFinished;
          protected bool IsExitingState;
          protected float StartTime;
     
          private readonly string _animBoolName;

          // Create a constructor for player so we can access all the function like Update, Exit,... 
          // in different State class that inherited from PlayerState
          protected PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
          {
               Player = player;
               StateMachine = stateMachine;
               PlayerData = playerData;
               _animBoolName = animBoolName;
               Core = player.Core;
          }

          public virtual void Enter()
          {
               // Each state will have different checks
               // Like jump state will check for ground
               DoChecks();
               
               // Run the animation with the same name in the animator
               Player.Anim.SetBool(_animBoolName, true);
               
               // Save the time when the player enter a state 
               StartTime = Time.time;
               IsAnimationFinished = false;
               IsExitingState = false;
          }
     
          public virtual void Exit()
          {
               // Set the current animation to false so we can change into a new animation                                                                                                                                                                                    
               Player.Anim.SetBool(_animBoolName, false);
               IsExitingState = true;
          }

          public virtual void LogicUpdate() {}
     
          public virtual void PhysicsUpdate() => DoChecks();
          protected virtual void DoChecks() {}
     
          public virtual void AnimationTrigger() {}
          public virtual void AnimationFinishTrigger() => IsAnimationFinished = true;

     }
}
