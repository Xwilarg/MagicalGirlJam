using System.Collections;
using UnityEngine;

namespace MagicalGirlJam.Character
{
    public abstract class ACharacter : MonoBehaviour
    {
        [SerializeField]
        private SO.CharacterInfo _info;

        protected float XMov { set; private get; }

        private Rigidbody2D _rb;

        private bool _canMove = true;
        private bool _canUseDash = true;

        private int _jumpRaycastLayer;

        private float ForwardDirection
            => (XMov < 0f || XMov == 0f && _rb.velocity.x < 0f) ? -1f : 1f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _jumpRaycastLayer = ~(1 << LayerMask.NameToLayer("Player"));
            Init();
        }

        protected virtual void Init()
        { }

        private void FixedUpdate()
        {
            if (!_canMove)
            {
                return;
            }
            _rb.velocity = new Vector2(XMov * Time.deltaTime * _info.Speed, _rb.velocity.y);
        }

        protected void Dash()
        {
            if (_canMove && _canUseDash)
            {
                _rb.AddForce(Vector2.right * _info.DashForce * ForwardDirection, ForceMode2D.Impulse);
                StartCoroutine(WaitAndReloadDash());
            }
        }

        protected void Jump()
        {
            if (_canMove)
            {
                var hit = Physics2D.Raycast(transform.position, Vector2.down, _info.JumpRaycastHeight, _jumpRaycastLayer);
                if (hit.collider != null)
                {
                    _rb.AddForce(Vector2.up * _info.JumpForce, ForceMode2D.Impulse);
                }
            }
        }

        protected void Attack()
        {
            if (_canMove)
            {
                var go = Instantiate(_info.BulletPrefab, transform.position, Quaternion.identity);
                go.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _info.BulletForce * ForwardDirection, ForceMode2D.Impulse);
                Destroy(go, 10f);
            }
        }

        private IEnumerator WaitAndReloadDash()
        {
            _canMove = false;
            _canUseDash = false;
            yield return new WaitForSeconds(_info.DashDuration);
            _canMove = true;
            yield return new WaitForSeconds(_info.DashReloadTime);
            _canUseDash = true;
        }
    }
}
