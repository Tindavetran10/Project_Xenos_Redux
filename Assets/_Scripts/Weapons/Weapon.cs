using System;
using _Scripts.CoreSystem;
using _Scripts.ScriptableObjectsScript;
using Unity.VisualScripting;
using UnityEngine;
using Timer = _Scripts.Utilities.Timer;


namespace _Scripts.Weapons
{
    // This script's basically a communicating hub between the player's attack state and the weapons
    public class Weapon : MonoBehaviour
    {
        // The amount of time that the combo will restart to the first animation
        [SerializeField] private float attackCounterResetCoolDown;
        
        
        // Get NumberOfAttacks and MovementSpeed from the scriptable object
        public WeaponDataSo Data { get; private set; }
        
        // Change the attack animation base on the number of player's input
        public int CurrentAttackCounter
        {
            get => _currentAttackCounter;
            // Reset the counter if it has reached the maximum number of attacks
            private set => _currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
        }
        private int _currentAttackCounter;
        private static readonly int Counter = Animator.StringToHash("counter");


        public event Action<bool> OnCurrentInputChange; 
        public bool CurrentInput
        {
            get => _currentInput;
            set
            {
                if (_currentInput != value)
                {
                    _currentInput = value;
                    OnCurrentInputChange?.Invoke(_currentInput);
                }
            }
        }
        private bool _currentInput;
        
        
        
        // Reference to the base under the CurrentWeapon prefabs
        private GameObject BaseGameObject { get; set; }         
        //public GameObject WeaponSpriteGameObject { get; private set; }
        private static readonly int Active = Animator.StringToHash("active");
        
        
        private Animator Anim { get; set; }
        public AnimationEventHandler EventHandler { get; private set; }
        private AnimationEventHandler _eventHandler;
        
        

        
        
        
        // Reference the Core that handle the normal movement of player
        public Core Core { get; private set; }

        private Timer _attackCounterResetTimer;
        
        public event Action OnEnter; 
        public event Action OnExit;

        
        public void Awake()
        {
            BaseGameObject = transform.Find("Base").GameObject();
            
            // Make references to the AnimationEventHandler script from the BaseGameObject
            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
            
            Anim = BaseGameObject.GetComponent<Animator>();
            _attackCounterResetTimer = new Timer(attackCounterResetCoolDown);
        }

        private void Update() => _attackCounterResetTimer.Tick();

        private void ResetAttackCounter() => CurrentAttackCounter = 0;
        
        public void Enter()
        {
            //print($"{transform.name} enter");
            
            _attackCounterResetTimer.StopTimer();
            
            // Check the boolean active from the unity editor 
            // Only change to the attack animation when the active is checked
            Anim.SetBool(Active, true);
            
            // Set the variable to the one in the Unity Editor
            Anim.SetInteger(Counter, CurrentAttackCounter);
            
            OnEnter?.Invoke();
        }
        
        private void Exit()
        {
            Anim.SetBool(Active, false);
            _attackCounterResetTimer.StartTimer();
            
            CurrentAttackCounter++;
            
            OnExit?.Invoke();
        }

        private void OnEnable()
        {
            EventHandler.OnCancel += Exit;
            EventHandler.OnFinish += Exit;
            _attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }
        
        private void OnDisable()
        {
            EventHandler.OnCancel -= Exit;
            EventHandler.OnFinish -= Exit;
            _attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
        }
        
        public void SetCore(Core core) => Core = core;
        public void SetData(WeaponDataSo data) => Data = data;
    }
}