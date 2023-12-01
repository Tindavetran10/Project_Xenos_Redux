using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class Weapon : MonoBehaviour
    {
        private Animator _anim;
        private GameObject _baseGameObject;
        private static readonly int Active = Animator.StringToHash("active");

        public void Awake()
        {
            _baseGameObject = transform.Find("Base").GameObject();
            _anim = _baseGameObject.GetComponent<Animator>();
        }

        public void Enter()
        {
            print($"{transform.name} enter");
            _anim.SetBool(Active, true);
        }
        
        
    }
}