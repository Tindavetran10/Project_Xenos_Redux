using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public event Action OnExit;
        
        private Animator _anim;
        private GameObject _baseGameObject;
        private static readonly int Active = Animator.StringToHash("active");

        private AnimationEventHandler _eventHandler;
        
        public void Awake()
        {
            _baseGameObject = transform.Find("Base").GameObject();
            _anim = _baseGameObject.GetComponent<Animator>();
            
            _eventHandler = _baseGameObject.GetComponent<AnimationEventHandler>();
        }
        
        public void Enter()
        {
            print($"{transform.name} enter");
            _anim.SetBool(Active, true);
        }
        
        private void Exit()
        {
            _anim.SetBool(Active, false);
            OnExit?.Invoke();
        }

        private void OnEnable() => _eventHandler.OnFinish += Exit;
        private void OnDisable() => _eventHandler.OnFinish -= Exit;
    }
}