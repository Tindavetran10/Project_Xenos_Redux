using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.CoreSystem.CoreComponents
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        //[SerializeField] private GameObject _damageParticles;

        private Stats _stats;
        //private CoreComp<ParticleManager> particleManager;
        
        public void Damage(float amount) {
            Debug.Log(Core.transform.parent.name + " Damaged!");
            _stats.Health.Decrease(amount);
            //particleManager.StartParticlesWithRandomRotation(damageParticles);
        }

        protected override void Awake()
        {
            base.Awake();

            _stats = Core.GetCoreComponent<Stats>();
            //particleManager = Core.GetCoreComponent<ParticleManager>();
        }
    }
}