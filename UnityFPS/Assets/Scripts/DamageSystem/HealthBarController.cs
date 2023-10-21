using UnityEngine;
using UnityEngine.UI;

namespace UnityFPS.DamageSystem
{
    public class HealthBarController : MonoBehaviour
    {
		[field: SerializeField]
        private Image HealthFillImage { get; set; }

		private IDamagable ObservedDamagable { get; set; }

		public void Initialize(IDamagable observedDamagable)
		{
			ObservedDamagable = observedDamagable;
			AttachToEvents(ObservedDamagable);
			RefreshHealth(ObservedDamagable.HealthObservableVariable.Value);
		}

		private void OnDisable()
		{
			DetachFromEvents(ObservedDamagable);
		}

		private void Update()
		{
			transform.LookAt(Camera.main.transform.position);
		}

		private void AttachToEvents(IDamagable damagable)
		{
			damagable.HealthObservableVariable.OnValueChanged += RefreshHealth;
		}

		private void DetachFromEvents(IDamagable damagable)
		{
			damagable.HealthObservableVariable.OnValueChanged -= RefreshHealth;
		}

		private void RefreshHealth(float value)
		{
			HealthFillImage.fillAmount = value / ObservedDamagable.MaxHealth;
		}
	}
}
