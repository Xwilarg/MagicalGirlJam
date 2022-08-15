using MagicalGirlJam.SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MagicalGirlJam.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo _info;

        private Rigidbody2D _rb;

        private float _mov;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_mov * Time.deltaTime * _info.Speed, _rb.velocity.y);
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>().x;
        }
    }
}
