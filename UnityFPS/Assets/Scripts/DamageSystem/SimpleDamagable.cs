using UnityEngine;
using UnityFPS.Tools.ReactiveVariable;

namespace UnityFPS.DamageSystem
{
    public class SimpleDamagable : MonoBehaviour, IDamagable
    {
		public IObservableVariable<float> HealthObservableVariable { get => Health; }

		[field: SerializeField]
		protected float MaxHealth { get; set; }

		protected FloatReactiveVariable Health { get; set; } = new FloatReactiveVariable();

		private void OnEnable()
		{
			Health.Value = MaxHealth;
		}

		public virtual void Hit(float damage)
		{
			Health.Value -= damage;

			if(Health.Value <= 0)
			{
				Destroy(gameObject);
			}
		}
    }
}
