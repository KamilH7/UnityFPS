using UnityFPS.Tools.ReactiveVariable;

namespace UnityFPS.DamageSystem
{
    public interface IDamagable
    {
        public MaterialType MaterialType { get; }
        public IObservableVariable<float> HealthObservableVariable { get; }
        public void Hit(float damage);
    }
}
