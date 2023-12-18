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
        // Get data from the scriptable object
        [field: SerializeField] public WeaponDataSO Data { get; private set; }
        
        // The amount of time that the combo will restart to the first animation
        [SerializeField] private float attackCounterResetCoolDown;

        // Change the attack animation base on the number of player's input
        public int CurrentAttackCounter
        {
            get => _currentAttackCounter;
            // Reset the counter if it has reached the maximum number of attacks
            private set => _currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
        }

        public event Action OnEnter; 
        public event Action OnExit;
        
        private Animator _anim;
        private AnimationEventHandler EventHandler { get; set; }
        
        // Reference to the base under the CurrentWeapon prefabs
        private GameObject BaseGameObject { get; set; }         
        //public GameObject WeaponSpriteGameObject { get; private set; }
        private static readonly int Active = Animator.StringToHash("active");
        
        public Core Core { get; private set; }

        private int _currentAttackCounter;
        private static readonly int Counter = Animator.StringToHash("counter");

        private Timer _attackCounterResetTimer;

        public void Awake()
        {
            BaseGameObject = transform.Find("Base").GameObject();
            //WeaponSpriteGameObject = transform.Find("WeaponSprite").GameObject();
            
            // Make references to the AnimationEventHandler script from the BaseGameObject
            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
            
            _anim = BaseGameObject.GetComponent<Animator>();
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
            _anim.SetBool(Active, true);
            
            // Set the variable to the one in the Unity Editor
            _anim.SetInteger(Counter, CurrentAttackCounter);
            
            OnEnter?.Invoke();
        }
        
        private void Exit()
        {
            _anim.SetBool(Active, false);
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
    }
}