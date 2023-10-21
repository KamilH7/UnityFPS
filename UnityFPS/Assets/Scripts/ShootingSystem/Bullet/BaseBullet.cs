using System.Collections.Generic;
using UnityEngine;
using UnityFPS.DamageSystem;
using UnityFPS.Tools.CollisionDetection;

namespace UnityFPS.ShootingSystem
{
    public abstract class BaseBullet : MonoBehaviour
    {
        [field: Header("Base References")]
        [field: SerializeField]
        protected Rigidbody BulletRigidbody { get; set; }
        [field: SerializeField]
        protected CollisionFilter EnemyCollisionFilter { get; set; }
        [field: SerializeField]
        protected CollisionFilter ObstacleCollisionFilter { get; set; }
        [field: SerializeField]
        protected Dictionary<MaterialType, float> DamageByMaterialMap { get; private set; }

        public virtual void Initialize(Dictionary<MaterialType, float> damageByMaterialMap, ShootingPoint shootingPoint, float power)
		{
            AttachToCollisionEvents();
            SetupBulletBody(shootingPoint, power);
            DamageByMaterialMap = damageByMaterialMap;
        }

        protected abstract void BulletCollidedWithDamagable(IDamagable hitDamagable);

        protected abstract void BulletCollidedWithObstacle(Collision collision);

        protected void SetupBulletBody(ShootingPoint shootingPoint, float power)
        {
            transform.position = shootingPoint.BulletOrigin;
            transform.forward = shootingPoint.BulletDirection;
            BulletRigidbody.velocity = shootingPoint.BulletDirection * power;
        }

        protected virtual void OnDisable()
		{
            DetachFromCollisionEvents();
		}

        protected void DestroyBullet()
		{
            Destroy(gameObject);
		}

        protected float GetDamageFromMaterial(MaterialType materialType)
        {
            if (DamageByMaterialMap.ContainsKey(materialType))
            {
                return DamageByMaterialMap[materialType];
            }
            else
            {
                return 0;
            }
        }

        private void AttachToCollisionEvents()
        {
            EnemyCollisionFilter.CollisionEnter.AddListener(OnBulletCollisionRegistered);
            ObstacleCollisionFilter.CollisionEnter.AddListener(BulletCollidedWithObstacle);
        }

        private void DetachFromCollisionEvents()
        {
            EnemyCollisionFilter.CollisionEnter.RemoveListener(OnBulletCollisionRegistered);
            ObstacleCollisionFilter.CollisionEnter.RemoveListener(BulletCollidedWithObstacle);
        }

        private IDamagable GetDamagableFromCollision(Collision collision)
        {
            return collision.collider.GetComponentInParent<IDamagable>();
        }

        private void OnBulletCollisionRegistered(Collision collision)
        {
            BulletCollidedWithDamagable(GetDamagableFromCollision(collision));
        }
    }
}