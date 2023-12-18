using System;
using _Scripts.Player.Input;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        private PlayerInputHandler _playerInputHandler;
        private Weapon _weapon;
        
        public event Action OnFinish;
        public event Action OnStartMovement;
        public event Action OnStopMovement;
        public event Action OnCancel;

        private void Start()
        {
            _playerInputHandler = GetComponentInParent<PlayerInputHandler>();
            _weapon = GetComponentInParent<Weapon>();
        }

        private void AnimationCancelTrigger()
        {
            if(_playerInputHandler.AttackInputs[_weapon.CurrentAttackCounter])
                OnCancel?.Invoke();
        }

        
        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
        private void StartMovementTrigger() => OnStartMovement?.Invoke();
        private void StopMovementTrigger() => OnStopMovement?.Invoke();
    }
}
