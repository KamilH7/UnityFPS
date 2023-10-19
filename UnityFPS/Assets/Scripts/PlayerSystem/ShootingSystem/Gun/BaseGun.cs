using UnityEngine;
using UnityFPS.Tools.ReactiveVariable;

namespace UnityFPS.PlayerSystem.ShootingSystem
{
    public abstract class BaseGun : MonoBehaviour
    {
        public IntReactiveVariable CurrentLoadedAmmo { get; private set; }
        public IntReactiveVariable CurrentReserveAmmo { get; private set; }

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

        public virtual void ShootInputStarted()
		{

		}

        public virtual void ShootInputStopped()
		{

		}

        public virtual void RealoadInput()
		{

		}
    }
}   