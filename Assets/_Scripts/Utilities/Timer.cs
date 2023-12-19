using System;
using UnityEngine;

namespace _Scripts.Utilities
{
    public class Timer
    {
        public event Action OnTimerDone;
        
        // Save the time when an event begin
        private float _startTime;
        
        // The length of the event 
        private readonly float _duration;
        
        // The time that check the duration has passed or not
        private float _targetTime;

        private bool _isActive;

        public Timer(float duration) => _duration = duration;
        
        // This function will start a timer 
        public void StartTimer()
        {
            // Get the time from Unity's realtime
            _startTime = Time.time;
            // Calculate the time check
            _targetTime = _startTime + _duration;
            // Set the activate state of the timer
            _isActive = true;
        }

        // Set the deactivate state of the timer
        public void StopTimer() => _isActive = false;

        public void Tick()
        {
            if(!_isActive) return;
            
            // When the Unity's realtime pass the the time check
            if (Time.time >= _targetTime)
            {
                // Do the Action and stop the timer
                OnTimerDone?.Invoke();
                StopTimer();
            }
        }
    }
}