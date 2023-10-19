
using UnityEngine;
using UnityFPS.Tools.ReactiveVariable;

namespace UnityFPS.DamageSystem
{
    public class BaseDamagable : MonoBehaviour, IDamagable
    {
		public IObservableVariable<float> HealthObservableVariable { get => Health; }

		protected FloatReactiveVariable Health { get; set; }

		public virtual void Hit(float damage)
		{
			Health.Value -= damage;

			if(Health.Value < 0)
			{
				Destroy(this);
			}
		}
    }
}
