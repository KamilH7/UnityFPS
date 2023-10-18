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

        public abstract void EnableInput();
        public abstract void DisableInput();
    }
}
