using UnityEngine;

namespace MagicalGirlJam.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/CharacterInfo", fileName = "CharacterInfo")]
    public class CharacterInfo : ScriptableObject
    {
        [Header("Stats")]
        public float Speed;

        public float DashForce;
        public float DashDuration;
        public float DashReloadTime;

        public float JumpForce;

        public float JumpRaycastHeight;

        public GameObject BulletPrefab;
        public float BulletForce;

        [Header("Attacks")]
        public AttackInfo MainAttack;
    }
}
