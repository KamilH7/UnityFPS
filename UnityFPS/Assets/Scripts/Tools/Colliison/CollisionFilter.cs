using UnityEngine;
using UnityEngine.Events;

namespace UnityFPS.Tools.CollisionDetection
{
    public class CollisionFilter : MonoBehaviour
    {
        [field: SerializeField]
        private CollisionNotifier BoundCollisionNotifier { get; set; }
        [field: SerializeField]
        private LayerMask CollisionLayerMask { get; set; }

        [field: SerializeField]
        public UnityEvent<Collision> CollisionEnter { get; set; }
        [field: SerializeField]
        public UnityEvent<Collision> CollisionExit { get; set; }
        [field: SerializeField]
        public UnityEvent<Collider> TriggerEnter { get; set; }
        [field: SerializeField]
        public UnityEvent<Collider> TriggerExit { get; set; }

        private void OnEnable()
		{
            BoundCollisionNotifier.CollisionEnter.AddListener(OnCollisionEnterNotifed);
            BoundCollisionNotifier.CollisionExit.AddListener(OnCollisionExitNotifed);
            BoundCollisionNotifier.TriggerEnter.AddListener(OnTriggerEnterNotified);
            BoundCollisionNotifier.TriggerExit.AddListener(OnTriggerExitNotified);
        }

        private void OnDisable()
		{
            BoundCollisionNotifier.CollisionEnter.RemoveListener(OnCollisionEnterNotifed);
            BoundCollisionNotifier.CollisionExit.RemoveListener(OnCollisionExitNotifed);
            BoundCollisionNotifier.TriggerEnter.RemoveListener(OnTriggerEnterNotified);
            BoundCollisionNotifier.TriggerExit.RemoveListener(OnTriggerExitNotified);
        }

        private void OnCollisionEnterNotifed(Collision collision)
		{
            if (IsInSpecifiedLayerMask(collision.collider))
            {
                CollisionEnter?.Invoke(collision);
            }
        }

        private void OnCollisionExitNotifed(Collision collision)
        {
            if (IsInSpecifiedLayerMask(collision.collider))
            {
                CollisionExit?.Invoke(collision);
            }
        }

        private void OnTriggerEnterNotified(Collider collider)
        {
            if (IsInSpecifiedLayerMask(collider))
            {
                TriggerEnter?.Invoke(collider);
            }
        }

        private void OnTriggerExitNotified(Collider collider)
        {
            if (IsInSpecifiedLayerMask(collider))
            {
                TriggerExit?.Invoke(collider);
            }
        }
        private bool IsInSpecifiedLayerMask(Collider collider)
        {
            if ((CollisionLayerMask.value & (1 << collider.transform.gameObject.layer)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
