using UnityEngine;
using UnityEngine.Events;
using UnityFPS.Tools.ReactiveVariable;

namespace UnityFPS.DamageSystem
{
    public class SimpleDamagable : MonoBehaviour, IDamagable
    {
		public IObservableVariable<float> HealthObservableVariable { get => Health; }

		[field: Header("References")]
		[field: SerializeField]
		private HealthBarController HealthBar { get; set; }

		[field: Header("Settings")]
		[field: SerializeField]
		public MaterialType MaterialType { get; private set; }
		[field: SerializeField]
		public float MaxHealth { get; private set; }
		[field: SerializeField]
		private UnityEvent OnDeath { get; set; }

		protected FloatReactiveVariable Health { get; set; } = new FloatReactiveVariable();

		private void OnEnable()
		{
			HealthBar.Initialize(this);
			Health.Value = MaxHealth;
		}

		public virtual void Hit(float damage)
		{
			Health.Value -= damage;

			if(Health.Value <= 0)
			{
				OnDeath.Invoke();
				Destroy(gameObject);
			}
		}	
    }
}
