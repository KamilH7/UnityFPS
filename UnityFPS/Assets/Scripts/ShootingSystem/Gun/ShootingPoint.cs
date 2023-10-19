using UnityEngine;

namespace UnityFPS.ShootingSystem
{
    public class ShootingPoint : MonoBehaviour
    {
		public Vector3 BulletOrigin { get => transform.position; }
		public Vector3 BulletDirection { get => transform.forward; }

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, transform.forward * 5);
		}
	}
}
