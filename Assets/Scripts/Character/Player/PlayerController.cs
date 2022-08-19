using MagicalGirlJam.Character;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MagicalGirlJam.Player
{
    public class PlayerController : ACharacter
    {
        public void OnMovement(InputAction.CallbackContext value)
        {
            XMov = value.ReadValue<Vector2>().x;
        }

        public void OnDash(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                Dash();
            }
        }

        public void OnJump(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                Jump();
            }
        }

        public void OnAttack(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                Attack();
            }
        }
    }
}
