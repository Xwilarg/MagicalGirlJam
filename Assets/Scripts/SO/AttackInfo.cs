using UnityEngine;

namespace MagicalGirlJam.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/AttackInfo", fileName = "AttackInfo")]
    public class AttackInfo : ScriptableObject
    {
        public int Damage;
    }
}
