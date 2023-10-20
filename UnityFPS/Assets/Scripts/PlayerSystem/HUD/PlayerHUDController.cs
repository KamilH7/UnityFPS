using UnityEngine;
using UnityFPS.ShootingSystem;

namespace UnityFPS.PlayerSystem.HUD
{
    public class PlayerHUDController : MonoBehaviour
    {
        [field: SerializeField]
        private PlayerGunManager GunManager { get; set; }
        [field: SerializeField]
        private GunSlotManager HUDGunSlotManager { get; set; }
		[field: SerializeField]
		private CurrentGunInfoDisplayer CurrentGunInfo { get; set; }

		private void OnEnable()
		{
			AttachToEvents();

			HUDGunSlotManager.InjectGunData(GunManager.GetGunDatas());
			CurrentGunInfo.SetGunData(GunManager.CurrentGunData);
        }

		private void OnDisable()
		{
			DetachFromEvents();
		}

		private void AttachToEvents()
		{
			GunManager.OnCurrentGunChanged += OnCurrentGunChanged;
		}

        private void DetachFromEvents()
		{
			GunManager.OnCurrentGunChanged += OnCurrentGunChanged;
		}

		private void OnCurrentGunChanged(GunSlotNumber slotNumber, IGunData gunData)
		{
			HUDGunSlotManager.SelectSlot(slotNumber);
			CurrentGunInfo.SetGunData(gunData);
		}
	}
}
