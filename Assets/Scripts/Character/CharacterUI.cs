using TMPro;
using UnityEngine;

namespace MagicalGirlJam.Character
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _name, _damageIndictor;

        public void Init(string name)
        {
            _name.text = name;
        }

        public void SetDamage(int damage)
        {
            _damageIndictor.text = $"{damage}%";

            var scale = damage / 500f;
            if (scale > 1f)
            {
                scale = 1f;
            }

            _damageIndictor.color = new Color(
                r: scale,
                g: 1 - scale,
                b: 0,
                a: 255
            );
        }
    }
}
