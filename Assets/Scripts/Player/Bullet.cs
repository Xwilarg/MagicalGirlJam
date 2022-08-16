using MagicalGirlJam.Enemy;
using UnityEngine;

namespace MagicalGirlJam.Player
{
    public class Bullet : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                collision.collider.GetComponent<AEnemy>().TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
