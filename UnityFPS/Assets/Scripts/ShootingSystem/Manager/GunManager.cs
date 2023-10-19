using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityFPS.PlayerSystem.InputSystem;
using UnityFPS.Tools.ReactiveVariable;

namespace UnityFPS.ShootingSystem
{
	public class GunManager : BaseInputController
	{
		public IObservableVariable<BaseGun> CurrentGunObservableVariable { get => CurrentGun; }

		[OdinSerialize]
		private Dictionary<GunSlot, BaseGun> GunBySlotMap { get; set; }

		private BaseGunReactiveVariable CurrentGun { get; set; } = new BaseGunReactiveVariable();

		private void Awake()
		{
			foreach (var gun in GunBySlotMap)
			{
				gun.Value.Initialize();
			}

			ChooseGun(GunSlot.SLOT_1);
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
			CurrentGun.Value.RealoadInput();
		}

		private void OnShootInputStarted(InputAction.CallbackContext actionValue)
		{
			CurrentGun.Value.ShootInputStarted();
		}

		private void OnShootInputStopped(InputAction.CallbackContext actionValue)
		{
			CurrentGun.Value.ShootInputStopped();
		}

		private void OnSlotOneInput(InputAction.CallbackContext actionValue)
		{
			ChooseGun(GunSlot.SLOT_1);
		}

		private void OnSlotTwoInput(InputAction.CallbackContext actionValue)
		{
			ChooseGun(GunSlot.SLOT_2);
		}

		private void OnSlotThreeInput(InputAction.CallbackContext actionValue)
		{
			ChooseGun(GunSlot.SLOT_3);
		}

		private void ChooseGun(GunSlot gunSlot)
		{
			if (CurrentGun.Value != null)
			{
				HideGun(CurrentGun.Value);
			}

			CurrentGun.Value = GunBySlotMap[gunSlot];
			ShowGun(CurrentGun.Value);
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
