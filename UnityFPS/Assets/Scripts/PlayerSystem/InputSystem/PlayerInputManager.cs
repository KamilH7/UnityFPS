using System.Collections.Generic;
using UnityEngine;

namespace UnityFPS.PlayerSystem.InputSystem
{
    public class PlayerInputManager : MonoBehaviour
    {
		public PlayerInputActions Actions { get; private set; } = null;

		[field: SerializeField]
		private List<BaseInputController> InputControllers { get; set; }

		private void Awake()
		{
			Actions = new PlayerInputActions();

			foreach (BaseInputController inputController in InputControllers)
			{
				inputController.Initialize(this);
			}
		}

		private void OnEnable()
		{
			EnableInput();

			foreach(BaseInputController inputController in InputControllers)
			{
				inputController.EnableInput();
			}
		}

		private void OnDisable()
		{
			DisableInput();

			foreach (BaseInputController inputController in InputControllers)
			{
				inputController.DisableInput();
			}
		}

		private void EnableInput()
		{
			Actions.Enable();
		}

		private void DisableInput()
		{
			Actions.Disable();
		}
	}
}
