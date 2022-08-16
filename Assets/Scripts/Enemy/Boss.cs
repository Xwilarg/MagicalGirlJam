using UnityEngine;
using UnityEngine.UI;

namespace MagicalGirlJam.Enemy
{
    public class Boss : AEnemy
    {
        [SerializeField]
        private Image _healthBar;

        private float _maxValue;

        private void Awake()
        {
            _maxValue = _healthBar.rectTransform.sizeDelta.x;
        }

        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);

            _healthBar.rectTransform.sizeDelta = new Vector2(_health * _maxValue / _maxHealth, _healthBar.rectTransform.sizeDelta.y);
        }
    }
}
