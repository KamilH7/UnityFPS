using System.Collections;
using UnityEngine;
using UnityFPS.Tools.ReactiveVariable;

namespace UnityFPS.ShootingSystem
{
    public abstract class BaseGun : MonoBehaviour
    {
        public IntReactiveVariable CurrentLoadedAmmo { get; private set; } = new IntReactiveVariable();
        public IntReactiveVariable CurrentReserveAmmo { get; private set; } = new IntReactiveVariable();

        [field: Header("Base References")]
        [field: SerializeField]
        protected BaseBullet BulletPrefab { get; set; }
        [field: SerializeField]
        protected ShootingPoint BulletShootingPoint { get; set; }

        [field: Header("Base Settings")]
        [field: SerializeField]
        protected int MagazineSize { get; set; }
        [field: SerializeField]
        protected int MaxReserveAmmo { get;  set; }
        [field: SerializeField]
        protected float MinTimeBetweenShots { get; set; }

        protected bool IsShootingOnCooldown { get; set; } = false;

        protected WaitForSeconds ShootCooldownYieldInstruction { get; set; }

        public virtual void Initialize()
		{
            CurrentLoadedAmmo.Value = MagazineSize;
            CurrentReserveAmmo.Value = MaxReserveAmmo;

            ShootCooldownYieldInstruction = new WaitForSeconds(MinTimeBetweenShots);
        }

        public virtual void ShootInputStarted()
		{
			if (IsShootingOnCooldown)
			{
                return;
			}

            StartCoroutine(ShootCooldownCoroutine());
		}

        public virtual void ShootInputStopped()
		{

		}

        public virtual void RealoadInput()
		{

		}

        protected virtual IEnumerator ShootCooldownCoroutine()
        {
            IsShootingOnCooldown = true;
            yield return ShootCooldownYieldInstruction;
            IsShootingOnCooldown = false;
        }

        protected virtual void InstantiateBullet(float power, float damage)
		{
            Instantiate(BulletPrefab);
            BulletPrefab.Initialize(BulletShootingPoint, power, damage);
        }
    }
}   