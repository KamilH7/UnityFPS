using UnityEngine;

namespace UnityFPS.PlayerSystem.ShootingSystem
{
    public class ShootingPoint : MonoBehaviour
    {
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, transform.forward * 5);
		}
	}
}
