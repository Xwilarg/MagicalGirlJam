using UnityEngine;

namespace MagicalGirlJam.Character
{
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
        }

        [SerializeField]
        private Transform _playerUIContainer;

        [SerializeField]
        private GameObject _characterUIPrefab;

        public void Init(ACharacter player)
        {
            var go = Instantiate(_characterUIPrefab, _playerUIContainer);
            var cUi = go.GetComponent<CharacterUI>();
            player.CharaUI = cUi;
            cUi.Init($"Player {GameObject.FindGameObjectsWithTag("Player").Length}");
        }
    }
}
