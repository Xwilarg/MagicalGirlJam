using MagicalGirlJam.AI;
using MagicalGirlJam.Player;
using MagicalGirlJam.SO;
using UnityEngine;

namespace MagicalGirlJam.Character
{
    public class Bullet : MonoBehaviour
    {
        public AttackInfo AttackInfo { set; private get; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                ACharacter charac;
                charac = collision.collider.GetComponent<PlayerController>();
                if (charac == null)
                {
                    charac = collision.collider.GetComponent<Enemy>();
                }
                charac.TakeDamage(AttackInfo.Damage);
            }
            Destroy(gameObject);
        }
    }
}
