using TMPro;
using UnityEngine;

namespace MagicalGirlJam.Character
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _name, damageIndictor;

        public void Init(string name)
        {
            _name.text = name;
        }
    }
}
