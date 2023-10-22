using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityFPS.PlayerSystem.InputSystem;
using UnityFPS.Tools.CollisionDetection;
using UnityFPS.Tools.Raycaster;

namespace UnityFPS.PlayerSystem.MovementSystem
{
	public class PlayerMovementController : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private Rigidbody PlayerRigidbody { get; set; }
		[field: SerializeField]
		private SimpleRaycaster GroundDetectionRaycaster { get; set; }
		[field: SerializeField]
		private InputManager PlayerInputManager { get; set; }

		[field: Header("Settings")]
		[field: SerializeField]
		private float Speed { get; set; }
		[field: SerializeField]
		private float JumpPower { get; set; }

		[field: Header("Runtime Values")]
		[field: SerializeField, ReadOnly]
		private Vector2 CurrentInput { get; set; }
		[field: SerializeField, ReadOnly]
		private bool IsGrounded { get; set; } = true;

		private void Update()
		{
			ApplyInput();
		}

		private void OnEnable()
		{
			AttachToEvents();
		}

		private void OnDisable()
		{
			DetachFromEvents();
		}

		private void ApplyInput()
		{
			Vector3 newVelocity = TranslateInputToLocalDirection() * Speed * CurrentInput.magnitude;
			newVelocity.y = PlayerRigidbody.velocity.y;
			PlayerRigidbody.velocity = newVelocity;
		}

		private void AttachToEvents()
		{
			PlayerInputManager.Actions.Movement.Walk.performed += OnPlayerWalk;
			PlayerInputManager.Actions.Movement.Walk.canceled += OnPlayerStopWalk;
			PlayerInputManager.Actions.Movement.Jump.performed += OnPlayerJump;

			GroundDetectionRaycaster.OnRaycastHit.AddListener(OnPlayerCollidedWithGround);
			GroundDetectionRaycaster.OnRaycastMissed.AddListener(OnPlayerStoppedCollidingWithGround);
		}

		private void DetachFromEvents()
		{
			PlayerInputManager.Actions.Movement.Walk.performed -= OnPlayerWalk;
			PlayerInputManager.Actions.Movement.Walk.canceled -= OnPlayerStopWalk;
			PlayerInputManager.Actions.Movement.Jump.performed -= OnPlayerJump;

			GroundDetectionRaycaster.OnRaycastHit.RemoveListener(OnPlayerCollidedWithGround);
			GroundDetectionRaycaster.OnRaycastMissed.RemoveListener(OnPlayerStoppedCollidingWithGround);
		}

		private void OnPlayerWalk(InputAction.CallbackContext actionValue)
		{
			CurrentInput = actionValue.ReadValue<Vector2>();
		}

		private void OnPlayerStopWalk(InputAction.CallbackContext actionValue)
		{
			CurrentInput = Vector2.zero;
		}

		private void OnPlayerJump(InputAction.CallbackContext inputValue)
		{
			if (IsGrounded)
			{
				PlayerRigidbody.AddForce(JumpPower * Vector3.up, ForceMode.Impulse);
			}
		}

		private void OnPlayerCollidedWithGround(Collider collision)
		{
			IsGrounded = true;
		}

		private void OnPlayerStoppedCollidingWithGround()
		{
			IsGrounded = false;
		}

		private Vector3 TranslateInputToLocalDirection()
		{
			Vector3 localDirectionBasedOnInput = Vector3.zero;

			localDirectionBasedOnInput += (CurrentInput.y > 0 ? transform.forward : -transform.forward) * Mathf.Abs(CurrentInput.y);
			localDirectionBasedOnInput += (CurrentInput.x > 0 ? transform.right : -transform.right) * Mathf.Abs(CurrentInput.x);

			return localDirectionBasedOnInput;
		}
	}
}
