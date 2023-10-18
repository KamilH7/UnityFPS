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
		private Vector3 MoveVector { get; set; }
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
			ApplyMoveVector();
		}

		private void ApplyMoveVector()
		{
			Vector3 currentVelocity = PlayerRigidbody.velocity;

			float newXVelocity = Mathf.Abs(MoveVector.x) > Mathf.Abs(currentVelocity.x) ? MoveVector.x : currentVelocity.x;
			float newZVelocity = Mathf.Abs(MoveVector.z) > Mathf.Abs(currentVelocity.z) ? MoveVector.z : currentVelocity.z;

			PlayerRigidbody.velocity = new Vector3(newXVelocity, currentVelocity.y, newZVelocity);
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
			Vector2 input = actionValue.ReadValue<Vector2>();
			Vector3 moveDirection = new Vector3(input.x, 0, input.y);
			MoveVector = moveDirection * Speed;
		}

		private void OnPlayerStopWalk(InputAction.CallbackContext actionValue)
		{
			MoveVector = Vector3.zero;
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
	}
}
