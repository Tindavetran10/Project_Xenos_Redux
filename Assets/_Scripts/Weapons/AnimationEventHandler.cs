using System;
using _Scripts.Player.Input;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        #region AnimationCancel Trigger requirements
        private PlayerInputHandler _playerInputHandler;
        private Weapon _weapon;
        private int _currentAttackIndex;
        #endregion
        public event Action OnFinish;
        public event Action OnStartMovement;
        public event Action OnStopMovement;
        public event Action OnAttackAction;
        public event Action OnCancel;
        public event Action<AttackPhases> OnEnterAttackPhases;
        
        public event Action<bool> OnFlipSetActive; 

        private void Start()
        {
            _playerInputHandler = GetComponentInParent<PlayerInputHandler>();
            _weapon = GetComponentInParent<Weapon>();
            _currentAttackIndex = _weapon.CurrentAttackCounter;
        }

        private void AnimationCancelTrigger()
        {
            if (_playerInputHandler.AttackInputs[_currentAttackIndex] || 
                _playerInputHandler.NormInputX == 1 || _playerInputHandler.NormInputX == -1 ||
                _playerInputHandler.JumpInput ||
                _playerInputHandler.DashInput) 
                OnCancel?.Invoke();
        }
        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
        private void StartMovementTrigger() => OnStartMovement?.Invoke();
        private void StopMovementTrigger() => OnStopMovement?.Invoke();
        private void AttackActionTrigger() => OnAttackAction?.Invoke();
        
        private void EnterAttackPhase(AttackPhases phase) => OnEnterAttackPhases?.Invoke(phase);

        private void SetFlipActive() => OnFlipSetActive?.Invoke(true);
        private void SetFlipInActive() => OnFlipSetActive?.Invoke(false);
    }
}
