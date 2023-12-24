using _Scripts.Interfaces;

namespace _Scripts.CoreSystem.CoreComponents
{
    public class PoiseDamageReceiver : CoreComponent, IPoiseDamageable
    {
        private Stats _stats;
        
        public void DamagePoise(float amount) => _stats.Poise.Decrease(amount);

        protected override void Awake()
        {
            base.Awake();
            _stats = Core.GetCoreComponent<Stats>();
        }
    }
}