using _Scripts.Enemies.EnemyState;
using UnityEngine;

namespace _Scripts.Intermediaries
{
    public class AnimationToStateMachine : MonoBehaviour
    {
        public AttackState AttackState;

        private void TriggerAttack() => AttackState.TriggerAttack();
        private void FinishAttack() => AttackState.FinishAttack();
    }
}
