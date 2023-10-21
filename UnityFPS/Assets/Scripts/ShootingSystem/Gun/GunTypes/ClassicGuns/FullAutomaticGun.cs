namespace UnityFPS.ShootingSystem
{
    public class FullAutomaticGun : ClassicGun
    {
        private bool IsInputPressed { get; set; }

        public override void ShootInputStarted()
        {
            base.ShootInputStarted();

            IsInputPressed = true;
        }

		public override void ShootInputStopped()
		{
			base.ShootInputStopped();

            IsInputPressed = false;
        }

        protected virtual void Update()
		{
            if (CanShoot())
			{
                ShootOnce();
			}
		}

        protected override bool CanShoot()
        {
            return base.CanShoot() && IsInputPressed;
        }
    }
}
