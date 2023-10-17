using UnityEngine;

namespace UnityFPS.PlayerSystem.InputSystem
{
    public class PlayerInputController : MonoBehaviour
    {
        [field: SerializeField]
        private PlayerInputActions InputActions { get; set; }

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
			InputActions.Enable();
		}

		private void DisableInput()
		{
			InputActions.Disable();
		}
	}
}
