using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityFPS.PlayerSystem.InputSystem;

namespace UnityFPS.ShootingSystem
{
	public class PlayerGunManager : BaseInputController
	{
		public event Action<GunSlotNumber, IGunData> OnCurrentGunChanged;
		public IGunData CurrentGunData => CurrentGun;
		public GunSlotNumber CurrentGunSlot { get; private set; }

		[OdinSerialize]
		private Dictionary<GunSlotNumber, BaseGun> GunBySlotMap { get; set; }
		private BaseGun CurrentGun { get; set; }

		public IEnumerable<(GunSlotNumber, IGunData)> GetGunDatas()
		{
			foreach (KeyValuePair<GunSlotNumber, BaseGun> gunBySlot in GunBySlotMap)
			{
				yield return (gunBySlot.Key, gunBySlot.Value);
			}
		}

		private void Awake()
		{
			foreach (KeyValuePair<GunSlotNumber, BaseGun> gun in GunBySlotMap)
			{
				gun.Value.Initialize();
			}

			ChooseGun(GunSlotNumber.SLOT_1);
		}

		protected override void AttachToInputEvents()
		{
			InputManager.Actions.Gun.Reload.performed += OnRealoadInput;
			InputManager.Actions.Gun.Shoot.performed += OnShootInputStarted;
			InputManager.Actions.Gun.Shoot.canceled += OnShootInputStopped;
			InputManager.Actions.Gun.GunSlot1.performed += OnSlotOneInput;
			InputManager.Actions.Gun.GunSlot2.performed += OnSlotTwoInput;
			InputManager.Actions.Gun.GunSlot3.performed += OnSlotThreeInput;
		}

		protected override void DetachFromInputEvents()
		{
			InputManager.Actions.Gun.Reload.performed -= OnRealoadInput;
			InputManager.Actions.Gun.Shoot.performed -= OnShootInputStarted;
			InputManager.Actions.Gun.Shoot.canceled -= OnShootInputStopped;
			InputManager.Actions.Gun.GunSlot1.performed -= OnSlotOneInput;
			InputManager.Actions.Gun.GunSlot2.performed -= OnSlotTwoInput;
			InputManager.Actions.Gun.GunSlot3.performed -= OnSlotThreeInput;
		}

		private void OnRealoadInput(InputAction.CallbackContext actionValue)
		{
			CurrentGun.RealoadInput();
		}

		private void OnShootInputStarted(InputAction.CallbackContext actionValue)
		{
			CurrentGun.ShootInputStarted();
		}

		private void OnShootInputStopped(InputAction.CallbackContext actionValue)
		{
			CurrentGun.ShootInputStopped();
		}

		private void OnSlotOneInput(InputAction.CallbackContext actionValue)
		{
			ChooseGun(GunSlotNumber.SLOT_1);
		}

		private void OnSlotTwoInput(InputAction.CallbackContext actionValue)
		{
			ChooseGun(GunSlotNumber.SLOT_2);
		}

		private void OnSlotThreeInput(InputAction.CallbackContext actionValue)
		{
			ChooseGun(GunSlotNumber.SLOT_3);
		}

		private void ChooseGun(GunSlotNumber gunSlot)
		{
			CurrentGunSlot = gunSlot;

			if (CurrentGun != null)
			{
				HideGun(CurrentGun);
			}

			CurrentGun = GunBySlotMap[gunSlot];

			ShowGun(CurrentGun);

			OnCurrentGunChanged?.Invoke(CurrentGunSlot, CurrentGun);
		}

		private void HideGun(BaseGun gun)
		{
			gun.gameObject.SetActive(false);
		}

		private void ShowGun(BaseGun gun)
		{
			gun.gameObject.SetActive(true);
		}
	}
}
