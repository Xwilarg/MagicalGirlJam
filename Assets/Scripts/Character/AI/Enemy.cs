using MagicalGirlJam.Character;
using System.Collections;
using UnityEngine;

namespace MagicalGirlJam.AI
{
    public class Enemy : ACharacter
    {
        protected override void Init()
        {
            StartCoroutine(WaitAndShoot());
        }

        private IEnumerator WaitAndShoot()
        {
            while (true)
            {
                yield return new WaitForSeconds(2f);
                Attack();
            }
        }
    }
}
