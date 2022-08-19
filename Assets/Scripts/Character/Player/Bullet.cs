using UnityEngine;

namespace MagicalGirlJam.Player
{
    public class Bullet : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Debug.Log("Hit registered");
            }
            Destroy(gameObject);
        }
    }
}
