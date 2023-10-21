using UnityEngine;
using UnityFPS.DamageSystem;

namespace UnityFPS.ShootingSystem
{
    public class ClassicBullet : BaseBullet
    {
        protected override void BulletCollidedWithDamagable(IDamagable hitDamagable)
        {
            hitDamagable.Hit(GetDamageFromMaterial(hitDamagable.MaterialType));
            DestroyBullet();
        }

		protected override void BulletCollidedWithObstacle(Collision collision)
		{
            DestroyBullet();
		}
	}
}
