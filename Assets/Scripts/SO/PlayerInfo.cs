using UnityEngine;

namespace MagicalGirlJam.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/PlayerInfo", fileName = "PlayerInfo")]
    public class PlayerInfo : ScriptableObject
    {
        public float Speed;
        public float DashForce;
        public float DashDuration;
        public float DashReloadTime;
    }
}
