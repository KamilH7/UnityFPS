using UnityEngine;

namespace UnityFPS.PlayerSystem.InputSystem
{
    public class PlayerInputController : MonoBehaviour
    {
		public PlayerInputActions Actions { get; private set; } = null;

		private void Awake()
		{
			Actions = new PlayerInputActions();
		}

		private void OnEnable()
		{
			EnableInput();
		}

		private void OnDisable()
		{
			DisableInput();
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
