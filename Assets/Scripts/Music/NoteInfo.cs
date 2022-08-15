using TMPro;
using UnityEngine;

namespace MagicalGirlJam.Music
{
    public class NoteInfo : MonoBehaviour
    {
        public float Speed { set; private get; }
        public NoteData Data
        {
            set
            {
                GetComponentInChildren<TMP_Text>().text = value.Direction.ToString()[0].ToString();
            }
        }

        private void Update()
        {
            transform.Translate(Vector3.left * Time.deltaTime * Speed);
        }
    }
}
