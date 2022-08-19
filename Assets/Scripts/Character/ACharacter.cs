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

        /// <summary>
        /// Is the character able to do anything
        /// </summary>
        private bool _canMove = true;
        /// <summary>
        /// Can the dash be used
        /// </summary>
        private bool _canUseDash = true;
        /// <summary>
        /// When true, the character is considered as flying
        /// </summary>
        private bool _isFlyingOverride;

        private int _jumpRaycastLayer;
        private int _dodgeLayer;
        private int _baseLayer;

        private int _damagePercent;

        public CharacterUI CharaUI { private get; set; }

        /// <summary>
        /// Get the forward direction of the player (-1 if left or 1 if right)
        /// </summary>
        private float ForwardDirection
            => (XMov < 0f || XMov == 0f && _rb.velocity.x < 0f) ? -1f : 1f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _jumpRaycastLayer = ~(1 << LayerMask.NameToLayer("Player"));
            _baseLayer = gameObject.layer;
            _dodgeLayer = LayerMask.NameToLayer("PlayerDodge");
        }

        private void Start()
        {
            CharacterManager.Instance.Init(this);
            Init();
        }

        /// <summary>
        /// Method called by children class to init themselves
        /// </summary>
        protected virtual void Init()
        { }

        private void FixedUpdate()
        {
            if (!_canMove)
            {
                return;
            }
            if (IsOnFloor)
            {
                _rb.velocity = new Vector2(XMov * Time.deltaTime * _info.Speed, _rb.velocity.y);
            }
            else
            {
                var maxSpeed = _info.Speed * Time.deltaTime;
                var curr = _info.Speed * Time.deltaTime * XMov * .01f + _rb.velocity.x;
                if (Mathf.Abs(curr) < maxSpeed)
                {
                    _rb.velocity = new Vector2(curr, _rb.velocity.y);
                }
            }
        }

        public void TakeDamage(Vector2 impactPoint, int damage)
        {
            _damagePercent += damage;
            CharaUI.SetDamage(_damagePercent);

            var direction = transform.position.x > impactPoint.x ? 1f : -1f;
            _rb.AddForce(new Vector2(direction, 1.5f).normalized * 5f * (_damagePercent / 100f), ForceMode2D.Impulse);

            StartCoroutine(TakeHit());
        }

        /// <summary>
        /// Called after dash, reenable player controls after some time and then reload dash
        /// </summary>
        private IEnumerator WaitAndReloadDash()
        {
            _canMove = false;
            _canUseDash = false;
            gameObject.layer = _dodgeLayer;
            yield return new WaitForSeconds(_info.DashDuration);
            _canMove = true;
            gameObject.layer = _baseLayer;
            yield return new WaitForSeconds(_info.DashReloadTime);
            _canUseDash = true;
        }

        private IEnumerator TakeHit()
        {
            _isFlyingOverride = true;
            yield return new WaitForSeconds(.5f);
            _isFlyingOverride = false;
        }

        private bool IsOnFloor
        {
            get
            {
                if (_isFlyingOverride)
                {
                    return false;
                }
                var hit = Physics2D.Raycast(transform.position, Vector2.down, _info.JumpRaycastHeight, _jumpRaycastLayer);
                return hit.collider != null;
            }
        }

        /// <summary>
        /// Dash allow for player to gain more speed in a direction and dodge attacks but prevent player from moving
        /// </summary>
        protected void Dash()
        {
            if (_canMove && _canUseDash)
            {
                _rb.AddForce(Vector2.right * _info.DashForce * ForwardDirection, ForceMode2D.Impulse);
                StartCoroutine(WaitAndReloadDash());
            }
        }

        /// <summary>
        /// Jump, launch player up
        /// </summary>
        protected void Jump()
        {
            if (_canMove && IsOnFloor)
            {
                _rb.AddForce(Vector2.up * _info.JumpForce, ForceMode2D.Impulse);
            }
        }

        /// <summary>
        /// Attack, for now throw a sphere in front of the player
        /// </summary>
        protected void Attack()
        {
            if (_canMove)
            {
                var go = Instantiate(_info.BulletPrefab, transform.position + Vector3.right * ForwardDirection, Quaternion.identity);
                go.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _info.BulletForce * ForwardDirection, ForceMode2D.Impulse);
                go.GetComponent<Bullet>().AttackInfo = _info.MainAttack;
                Destroy(go, 10f);
            }
        }
    }
}
