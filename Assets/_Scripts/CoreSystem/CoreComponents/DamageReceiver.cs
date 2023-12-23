using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.CoreSystem.CoreComponents
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        //[SerializeField] private GameObject _damageParticles;

        private CoreComp<Stats> _stats;
        //private CoreComp<ParticleManager> particleManager;
        
        public void Damage(float amount) {
            Debug.Log(Core.transform.parent.name + " Damaged!");
            _stats.Comp?.DecreaseHealth(amount);
            //particleManager.Comp?.StartParticlesWithRandomRotation(damageParticles);
        }

        protected override void Awake()
        {
            base.Awake();

            _stats = new CoreComp<Stats>(Core);
            //particleManager = new CoreComp<ParticleManager>(core);
        }
    }
}