using UnityEngine;
using UnityFPS.DamageSystem;

namespace UnityFPS.PlayerSystem.ShootingSystem
{
    public class ClassicBullet : BaseBullet
    {
        protected override void BulletCollidedWithDamagable(IDamagable hitDamagable)
        {
            hitDamagable.Hit(GunHitDamage);
            DestroyBullet();
        }

		protected override void BulletCollidedWithObstacle(Collision collision)
		{
            DestroyBullet();
		}
	}
}
