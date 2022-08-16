using UnityEngine;

namespace MagicalGirlJam.Enemy
{
    public abstract class AEnemy : MonoBehaviour
    {
        protected int _health = 10;
        protected int _maxHealth = 10;

        public virtual void TakeDamage(int amount)
        {
            _health -= amount;
            if (_health < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
