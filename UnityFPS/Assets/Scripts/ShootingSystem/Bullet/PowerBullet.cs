using System.Collections.Generic;
using UnityEngine;
using UnityFPS.DamageSystem;

namespace UnityFPS.ShootingSystem
{
    public class PowerBullet : BaseBullet
    {
		[field: SerializeField]
		private float MaxPower { get; set; }

		private float Power { get; set; }

		public override void Initialize(Dictionary<MaterialType, float> damageByMaterialMap, ShootingPoint shootingPoint, float power)
		{
			base.Initialize(damageByMaterialMap, shootingPoint, power);

			Power = power;
		}

		protected override void BulletCollidedWithDamagable(IDamagable hitDamagable)
        {
            hitDamagable.Hit(GetDamageFromMaterial(hitDamagable.MaterialType) * Power / MaxPower);
            DestroyBullet();
        }

		protected override void BulletCollidedWithObstacle(Collision collision)
		{
            DestroyBullet();
		}
	}
}
