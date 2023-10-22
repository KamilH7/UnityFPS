using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityFPS.PlayerSystem.InputSystem;

namespace UnityFPS.ShootingSystem
{
	public class PlayerGunManager : SerializedMonoBehaviour
	{
		public event Action<GunSlotNumber, IGunData> OnCurrentGunChanged;
		public IGunData CurrentGunData => CurrentGun;
		public GunSlotNumber? CurrentGunSlot { get; private set; } = null;
		
		[field: Header("References")]
		[field: SerializeField]
		private InputManager PlayerInputManager { get; set; }

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

		private void OnEnable()
		{
			AttachToInputEvents();
		}

		private void OnDisable()
		{
			DetachFromInputEvents();
		}

		private void AttachToInputEvents()
		{
			PlayerInputManager.Actions.Gun.Reload.performed += OnRealoadInput;
			PlayerInputManager.Actions.Gun.Shoot.performed += OnShootInputStarted;
			PlayerInputManager.Actions.Gun.Shoot.canceled += OnShootInputStopped;
			PlayerInputManager.Actions.Gun.GunSlot1.performed += OnSlotOneInput;
			PlayerInputManager.Actions.Gun.GunSlot2.performed += OnSlotTwoInput;
			PlayerInputManager.Actions.Gun.GunSlot3.performed += OnSlotThreeInput;
		}

		private void DetachFromInputEvents()
		{
			PlayerInputManager.Actions.Gun.Reload.performed -= OnRealoadInput;
			PlayerInputManager.Actions.Gun.Shoot.performed -= OnShootInputStarted;
			PlayerInputManager.Actions.Gun.Shoot.canceled -= OnShootInputStopped;
			PlayerInputManager.Actions.Gun.GunSlot1.performed -= OnSlotOneInput;
			PlayerInputManager.Actions.Gun.GunSlot2.performed -= OnSlotTwoInput;
			PlayerInputManager.Actions.Gun.GunSlot3.performed -= OnSlotThreeInput;
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
			if(gunSlot == CurrentGunSlot)
			{
				return;
			}

			CurrentGunSlot = gunSlot;

			if (CurrentGun != null)
			{
				HideGun(CurrentGun);
			}

			CurrentGun = GunBySlotMap[gunSlot];

			ShowGun(CurrentGun);

			OnCurrentGunChanged?.Invoke((GunSlotNumber) CurrentGunSlot, CurrentGun);
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
