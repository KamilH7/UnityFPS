using System.Collections;
using UnityEngine;

namespace UnityFPS.PlayerSystem.ShootingSystem
{
    public abstract class BaseReloadableGun : BaseGun
    {
        [field: SerializeField]
        protected float ReloadTime { get; set; }

        protected bool IsRealoadig { get; set; }

        public override void ShootInputStarted()
		{
			base.ShootInputStarted();

            if (IsRealoadig == true)
            {
                return;
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

		protected virtual IEnumerator ReloadCoroutine()
        {
            IsRealoadig = true;
            yield return new WaitForSeconds(ReloadTime);
            IsRealoadig = false;
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
    }
}
