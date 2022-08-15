using MagicalGirlJam.SO;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MagicalGirlJam.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo _info;

        private bool _hasControlOverPlayer = true;
        private bool _canUseDash = true;

        private Rigidbody2D _rb;

        private float _mov;

        private int _jumpRaycastLayer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _jumpRaycastLayer = ~(1 << LayerMask.NameToLayer("Player"));
        }

        private void FixedUpdate()
        {
            if (!_hasControlOverPlayer)
            {
                return;
            }
            _rb.velocity = new Vector2(_mov * Time.deltaTime * _info.Speed, _rb.velocity.y);
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>().x;
        }

        public void OnDash(InputAction.CallbackContext value)
        {
            if (value.performed && _hasControlOverPlayer && _canUseDash)
            {
                float dashForce = _info.DashForce * ((_mov < 0f || _mov == 0f && _rb.velocity.x < 0f) ? -1f : 1f);
                _rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
                StartCoroutine(WaitAndReloadDash());
            }
        }

        public void OnJump(InputAction.CallbackContext value)
        {
            if (value.performed && _hasControlOverPlayer)
            {
                var hit = Physics2D.Raycast(transform.position, Vector2.down, _info.JumpRaycastHeight, _jumpRaycastLayer);
                if (hit.collider != null)
                {
                    _rb.AddForce(Vector2.up * _info.JumpForce, ForceMode2D.Impulse);
                }
            }
        }

        private IEnumerator WaitAndReloadDash()
        {
            _hasControlOverPlayer = false;
            _canUseDash = false;
            yield return new WaitForSeconds(_info.DashDuration);
            _hasControlOverPlayer = true;
            yield return new WaitForSeconds(_info.DashReloadTime);
            _canUseDash = true;

        }
    }
}
