using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityFPS.PlayerSystem.InputSystem;

namespace UnityFPS.PlayerSystem.MovementSystem
{
	public class PlayerMovementController : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private Rigidbody PlayerRigidbody { get; set; }
		[field: SerializeField]
		private PlayerInputController PlayerInput { get; set; }

		[field: Header("Settings")]
		[field: SerializeField]
		private float Speed { get; set; }
		[field: SerializeField]
		private float JumpPower { get; set; }

		[field: Header("Runtime Values")]
		[field: SerializeField, ReadOnly]
		private Vector3 MoveVector { get; set; }
		[field: SerializeField, ReadOnly]
		private bool IsGrounded { get; set; }

		private void OnEnable()
		{
			AttachToEvents();
		}

		private void OnDisable()
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
			PlayerInput.Actions.Movement.Walk.performed += OnPlayerWalk;
			PlayerInput.Actions.Movement.Walk.canceled += OnPlayerStopWalk;
			PlayerInput.Actions.Movement.Jump.performed += OnPlayerJump;
		}

		private void DetachFromEvents()
		{
			PlayerInput.Actions.Movement.Walk.performed -= OnPlayerWalk;
			PlayerInput.Actions.Movement.Walk.canceled -= OnPlayerStopWalk;
			PlayerInput.Actions.Movement.Jump.performed -= OnPlayerJump;
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
			PlayerRigidbody.AddForce(JumpPower * Vector3.up, ForceMode.Impulse);
		}
	}
}
