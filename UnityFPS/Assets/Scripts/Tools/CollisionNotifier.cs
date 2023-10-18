using UnityEngine;
using UnityEngine.Events;

namespace UnityFPS.Tools.CollisionDetection
{
    public class CollisionNotifier : MonoBehaviour
    {
		[field: SerializeField]
        public UnityEvent<Collider> TriggerEnter { get; set; }
		[field: SerializeField]
		public UnityEvent<Collider> TriggerExit { get; set; }
		[field: SerializeField]
		public UnityEvent<Collision> CollisionEnter { get; set; }
		[field: SerializeField]
		public UnityEvent<Collision> CollisionExit { get; set; }

		private void OnCollisionEnter(Collision collision)
		{
			CollisionEnter?.Invoke(collision);
		}

		private void OnCollisionExit(Collision collision)
		{
			CollisionExit?.Invoke(collision);
		}

		private void OnTriggerEnter(Collider collider)
		{
			TriggerEnter?.Invoke(collider);
		}

		private void OnTriggerExit(Collider collider)
		{
			TriggerExit?.Invoke(collider);
		}
	}
}
