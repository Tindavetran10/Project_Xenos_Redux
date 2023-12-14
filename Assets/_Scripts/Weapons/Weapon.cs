using System;
using Unity.VisualScripting;
using UnityEngine;
using Timer = _Scripts.Utilities.Timer;


namespace _Scripts.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int numberOfAttacks;
        [SerializeField] private float attackCounterResetCoolDown;

        private int CurrentAttackCounter
        {
            get => _currentAttackCounter;
            set => _currentAttackCounter = value >= numberOfAttacks ? 0 : value;
        }
        
        public event Action OnExit;
        
        private Animator _anim;
        private GameObject _baseGameObject;
        private static readonly int Active = Animator.StringToHash("active");

        private AnimationEventHandler _eventHandler;

        private int _currentAttackCounter;
        private static readonly int Counter = Animator.StringToHash("counter");

        private Timer _attackCounterResetTimer;

        public void Awake()
        {
            _baseGameObject = transform.Find("Base").GameObject();
            _anim = _baseGameObject.GetComponent<Animator>();
            
            _eventHandler = _baseGameObject.GetComponent<AnimationEventHandler>();

            _attackCounterResetTimer = new Timer(attackCounterResetCoolDown);
        }

        private void Update() => _attackCounterResetTimer.Tick();

        private void ResetAttackCounter() => CurrentAttackCounter = 0;
        
        public void Enter()
        {
            print($"{transform.name} enter");
            
            _attackCounterResetTimer.StopTimer();
            
            _anim.SetBool(Active, true);
            _anim.SetInteger(Counter, CurrentAttackCounter);
        }
        
        private void Exit()
        {
            _anim.SetBool(Active, false);

            CurrentAttackCounter++;
            _attackCounterResetTimer.StartTimer();
            OnExit?.Invoke();
        }

        private void OnEnable()
        {
            _eventHandler.OnFinish += Exit;
            _attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }

        private void OnDisable()
        {
            _eventHandler.OnFinish -= Exit;
            _attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
        }
    }
}