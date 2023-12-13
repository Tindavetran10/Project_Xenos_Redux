using System;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinish;
        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
    }
}
