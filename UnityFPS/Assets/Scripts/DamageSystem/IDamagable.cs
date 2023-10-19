using UnityFPS.Tools.ReactiveVariable;

namespace UnityFPS.DamageSystem
{
    public interface IDamagable
    {
        public IObservableVariable<float> HealthObservableVariable { get; }
        public void Hit(float damage);
    }
}
