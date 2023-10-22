using UnityEngine;
using UnityEngine.Events;

namespace UnityFPS.Tools.Raycaster
{
    public class SimpleRaycaster : MonoBehaviour
    {
        [field: Header("Settings")]
        [field: SerializeField]
        private LayerMask RaycastLayerMask { get; set; }
        [field: SerializeField]
        private float RaycastLength { get; set; }

        [field: Header("Raycast Events")]
        [field: SerializeField]
        public UnityEvent<Collider> OnRaycastHit { get; private set; }
        [field: SerializeField]
        public UnityEvent OnRaycastMissed { get; private set; }

        void Update()
        {
            CastRay();
        }

        private void CastRay()
		{
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, RaycastLength, RaycastLayerMask))
			{
                OnRaycastHit?.Invoke(raycastHit.collider);
            }
			else
            {
                OnRaycastMissed?.Invoke();
            }
		}

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.02f);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * RaycastLength);
        }
    }
}
