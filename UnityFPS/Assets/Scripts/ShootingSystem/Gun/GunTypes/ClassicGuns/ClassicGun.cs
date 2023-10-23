using System.Collections;
using UnityEngine;

namespace UnityFPS.ShootingSystem
{
    public abstract class ClassicGun : BaseGun
    {
        [field: Header("Classic Settings")]
        [field: SerializeField]
        protected float ReloadTime { get; set; }
        [field: SerializeField]
        protected float BasePower { get; set; }
        [field: SerializeField]
        protected float GunShakePower { get; set; }

        [field: Header("Animation")]
        [field: SerializeField]
        protected Animator GunAnimator { get; set; }
        [field: SerializeField]
        protected string ReloadAnimationTrigger { get; set; }

        protected bool IsReloadig { get; set; }

        public override void ShootInputStarted()
		{
			base.ShootInputStarted();
        }

		public override void RealoadInput()
		{
			base.RealoadInput();

            if (CurrentReserveAmmo.Value > 0 && CurrentLoadedAmmo.Value != MagazineSize)
            {
                StartCoroutine(ReloadCoroutine());
            }
        }

        protected override bool CanShoot()
		{
            return base.CanShoot() && IsReloadig == false && HasAmmo() == true;
        }

        protected virtual void ShootOnce()
		{
            InstantiateBullet(BasePower);
            CurrentLoadedAmmo.Value -= 1;
            ApplyShootCooldown();
            ApplyGunShake(GunShakePower);
        }

		protected virtual IEnumerator ReloadCoroutine()
        {
            GunAnimator.SetTrigger(ReloadAnimationTrigger);
            IsReloadig = true;
            yield return new WaitForSeconds(ReloadTime);
            ApplyReloadedAmmo();
            IsReloadig = false;
        }

        protected void ApplyReloadedAmmo()
        {
            int missingAmmo = MagazineSize - CurrentLoadedAmmo.Value;
            int newReserveAmmo = CurrentReserveAmmo.Value - missingAmmo;

            if (newReserveAmmo < 0)
            {
                CurrentLoadedAmmo.Value = MagazineSize + newReserveAmmo;
                CurrentReserveAmmo.Value = 0;
            }
            else
            {
                CurrentLoadedAmmo.Value = MagazineSize;
                CurrentReserveAmmo.Value = newReserveAmmo;
            }
        }

        protected bool HasAmmo()
		{
            return CurrentLoadedAmmo.Value > 0;
		}
    }
}
