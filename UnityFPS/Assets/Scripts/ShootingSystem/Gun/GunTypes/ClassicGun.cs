using System.Collections;
using UnityEngine;

namespace UnityFPS.ShootingSystem
{
    public class ClassicGun : BaseGun
    {
        [field: Header("Classic Settings")]
        [field: SerializeField]
        protected float ReloadTime { get; set; }
        [field: SerializeField]
        protected float BasePower { get; set; }
        [field: SerializeField]
        protected float BaseDamage { get; set; }

        protected bool IsReloadig { get; set; }

        public override void ShootInputStarted()
		{
			base.ShootInputStarted();

            if(IsReloadig == false && HasAmmo() == true)
			{
                ShootOnce();
			}      
        }

		public override void RealoadInput()
		{
			base.RealoadInput();

            if (CurrentReserveAmmo.Value > 0)
            {
                StartCoroutine(ReloadCoroutine());
            }
        }

        protected virtual void ShootOnce()
		{
            InstantiateBullet(BasePower, BaseDamage);
            CurrentLoadedAmmo.Value -= 1;
        }

		protected virtual IEnumerator ReloadCoroutine()
        {
            IsReloadig = true;
            yield return new WaitForSeconds(ReloadTime);
            IsReloadig = false;
        }

        protected void ApplyReloadedAmmo()
        {
            int missingAmmo = MagazineSize - CurrentLoadedAmmo.Value;
            int newReserveAmmo = CurrentReserveAmmo.Value - missingAmmo;

            if (newReserveAmmo < 0)
            {
                CurrentLoadedAmmo.Value = MagazineSize - newReserveAmmo;
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
