namespace UnityFPS.ShootingSystem
{
    public class SemiAutomaticGun : ClassicGun
    {
        public override void ShootInputStarted()
		{
			base.ShootInputStarted();

			if (CanShoot())
			{
                ShootOnce();
            }
        }
    }
}
