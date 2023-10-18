using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityFPS.PlayerSystem.InputSystem.CameraSystem
{
    public class CameraMovementController : BaseInputController
    {
        [field: Header("References")]
        [field: SerializeField]
        private Camera PlayerCamera { get; set; }
        [field: SerializeField]
        private Transform PlayerTransform { get; set; }

        [field: Header("Settings")]
        [field: SerializeField]
        private bool InvertUpDownAxis{ get; set; }
        [field: SerializeField]
        private float LookSensitivity { get; set; }
        [field: SerializeField]
        private float CameraSmoothness { get; set; }
        [field: SerializeField]
        private float MaxLookAngle { get; set; }

        public override void EnableInput()
        {
            Cursor.lockState = CursorLockMode.Locked;
            AttachToEvents();
        }

        public override void DisableInput()
		{
            Cursor.lockState = CursorLockMode.None;
            DetachFromEvents();
        }

        private void AttachToEvents()
		{
            InputManager.Actions.Camera.Look.performed += OnPlayerLook;
        }

        private void DetachFromEvents()
		{
            InputManager.Actions.Camera.Look.performed -= OnPlayerLook;
        }

        private void OnPlayerLook(InputAction.CallbackContext actionValue)
        {
            Vector2 input = actionValue.ReadValue<Vector2>();

            if (InvertUpDownAxis)
            {
                input.y = -input.y;
            }

            Vector2 movementVector = input * LookSensitivity * Time.deltaTime;

            ApplyUpDownRotation(movementVector.y);
            ApplyLeftRightRotation(movementVector.x);
        }

		private void ApplyUpDownRotation(float rotationDelta)
		{
            Quaternion newRotation = PlayerCamera.transform.rotation * Quaternion.Euler(Vector3.right * rotationDelta);
            float newLookAngle = Quaternion.Angle(PlayerTransform.rotation, newRotation);

            if (newLookAngle < MaxLookAngle)
			{
                PlayerCamera.transform.rotation = newRotation;
            }
        }

        private void ApplyLeftRightRotation(float rotationDelta)
        {
            PlayerTransform.rotation *= Quaternion.Euler(Vector3.up * rotationDelta);
        }
    }
}
