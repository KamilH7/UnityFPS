using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityFPS.Tools.CollisionDetection;

namespace UnityFPS.PlayerSystem.InputSystem.MovementSystem
{
	public class PlayerMovementController : BaseInputController
	{
		[field: Header("References")]
		[field: SerializeField]
		private Rigidbody PlayerRigidbody { get; set; }
		[field: SerializeField]
		private CollisionFilter GroundCollisionFilter { get; set; }

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

		public override void EnableInput()
		{
			AttachToEvents();
		}

		public override void DisableInput()
		{
			DetachFromEvents();
		}

		private void Update()
		{
			ApplyInput();
		}

		private void ApplyInput()
		{
			Vector3 newVelocity = TranslateInputToLocalDirection() * Speed * CurrentInput.magnitude;
			newVelocity.y = PlayerRigidbody.velocity.y;
			PlayerRigidbody.velocity = newVelocity;
		}

		private void AttachToEvents()
		{
			InputManager.Actions.Movement.Walk.performed += OnPlayerWalk;
			InputManager.Actions.Movement.Walk.canceled += OnPlayerStopWalk;
			InputManager.Actions.Movement.Jump.performed += OnPlayerJump;

			GroundCollisionFilter.CollisionEnter.AddListener(OnPlayerCollidedWithGround);
			GroundCollisionFilter.CollisionExit.AddListener(OnPlayerStoppedCollidingWithGround);
		}

		private void DetachFromEvents()
		{
			InputManager.Actions.Movement.Walk.performed -= OnPlayerWalk;
			InputManager.Actions.Movement.Walk.canceled -= OnPlayerStopWalk;
			InputManager.Actions.Movement.Jump.performed -= OnPlayerJump;

			GroundCollisionFilter.CollisionEnter.RemoveListener(OnPlayerCollidedWithGround);
			GroundCollisionFilter.CollisionExit.RemoveListener(OnPlayerStoppedCollidingWithGround);
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

		private void OnPlayerCollidedWithGround(Collision collision)
		{
			IsGrounded = true;
		}

		private void OnPlayerStoppedCollidingWithGround(Collision collision)
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
