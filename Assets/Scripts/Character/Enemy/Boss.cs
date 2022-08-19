using UnityEngine;
using UnityEngine.UI;

namespace MagicalGirlJam.Enemy
{
    public class Boss : AEnemy
    {
        [SerializeField]
        private Image _healthBar;

        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);

            _healthBar.rectTransform.localScale = new Vector3((float)_health / _maxHealth, _healthBar.rectTransform.localScale.y, 1f);
        }
    }
}
