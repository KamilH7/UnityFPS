using UnityEngine;
using UnityEngine.InputSystem;
using UnityFPS.PlayerSystem.InputSystem;

namespace UnityFPS.PlayerSystem.CameraSystem
{
    public class CameraMovementController : MonoBehaviour
    {
        [field: Header("References")]
        [field: SerializeField]
        private Camera PlayerCamera { get; set; }
        [field: SerializeField]
        private Transform PlayerTransform { get; set; }
        [field: SerializeField]
        private InputManager PlayerInputManager { get; set; }

        [field: Header("Settings")]
        [field: SerializeField]
        private bool InvertUpDownAxis{ get; set; }
        [field: SerializeField]
        private float LookSensitivity { get; set; }
        [field: SerializeField]
        private float CameraSmoothness { get; set; }
        [field: SerializeField]
        private float MaxLookAngle { get; set; }

		private void OnEnable()
		{
            Cursor.lockState = CursorLockMode.Locked;
            AttachToInputEvents();
        }

		private void OnDisable()
		{
            Cursor.lockState = CursorLockMode.None;
            DetachFromInputEvents();
        }


        private void AttachToInputEvents()
		{
            PlayerInputManager.Actions.Camera.Look.performed += OnPlayerLook;
        }

        private void DetachFromInputEvents()
		{
            PlayerInputManager.Actions.Camera.Look.performed -= OnPlayerLook;
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
