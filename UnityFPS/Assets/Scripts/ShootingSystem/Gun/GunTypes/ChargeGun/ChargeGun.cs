using UnityEngine;

namespace UnityFPS.ShootingSystem
{
    public class ChargeGun : BaseGun
    {
        [field: Header("Charge Gun Settings")]
        [field: SerializeField]
        private float ChargePerSecond { get; set; }

        private bool IsCharging { get; set; }
        private float CurrentCharge { get; set; }
        private int StartChargeReserveAmmoAmount { get; set; }
        private int MaxCharge { get; set; }

        public override void Initialize()
		{
			base.Initialize();

            CurrentLoadedAmmo.Value = 0;
        }

		public override void ShootInputStarted()
        {
            base.ShootInputStarted();

            if (CanShoot() == false)
            {
                return;
            }

            StartChargeReserveAmmoAmount = CurrentReserveAmmo.Value;

            if (CurrentReserveAmmo.Value < MagazineSize)
			{
                MaxCharge = CurrentReserveAmmo.Value;
			}
			else
            {
                MaxCharge = MagazineSize;
            }

            IsCharging = true;
        }

        public override void ShootInputStopped()
        {
            base.ShootInputStarted();

             ShootChargedShot();
             IsCharging = false;
        }

        protected virtual void Update()
		{
			if (IsCharging)
			{
                CurrentCharge += ChargePerSecond * Time.deltaTime;
                CurrentCharge = Mathf.Clamp(CurrentCharge, 0, MaxCharge);
                AdjustAmmoToChargeState();
            }
		}

        private void AdjustAmmoToChargeState()
		{
            if(CurrentLoadedAmmo.Value != CurrentCharge)
            {
                CurrentLoadedAmmo.Value = (int) CurrentCharge;
                CurrentReserveAmmo.Value = StartChargeReserveAmmoAmount - (int) CurrentCharge;
            }
        }

        public void ShootChargedShot()
		{
            if(CurrentCharge > 0)
			{
                InstantiateBullet(CurrentCharge, CurrentCharge);
                ApplyShootCooldown();
                ApplyGunShake(CurrentCharge);
                CurrentCharge = 0;
                CurrentLoadedAmmo.Value = 0;
            }
        }
    }
}
