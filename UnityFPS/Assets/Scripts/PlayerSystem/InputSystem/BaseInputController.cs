using UnityEngine;

namespace UnityFPS.PlayerSystem.InputSystem
{
    public abstract class BaseInputController : MonoBehaviour
    {
        protected PlayerInputManager InputManager { get; set; }

        public void Initialize(PlayerInputManager playerInputManager)
		{
            InputManager = playerInputManager;
        }

        public virtual void EnableInput()
		{
            AttachToInputEvents();
		}

        public virtual void DisableInput()
		{
            DetachFromInputEvents();
		}

        protected abstract void AttachToInputEvents();

        protected abstract void DetachFromInputEvents();
    }
}   
