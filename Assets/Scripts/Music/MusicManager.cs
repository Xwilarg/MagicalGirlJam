using System;
using System.Linq;
using UnityEngine;

namespace MagicalGirlJam.Music
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _musicFile;

        [SerializeField]
        private GameObject _notePrefab;

        [SerializeField]
        private RectTransform _container;

        private MusicData _data;

        private float _timer;
        private int _dataIndex;

        private void Awake()
        {
            _data = new()
            {
                Notes = _musicFile.text.Replace("\r", "").Split('\n').Select(x => Parse(x)).ToArray()
            };
            SpawnNextNotes();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        private void SpawnNextNotes()
        {
            for (; _dataIndex < _data.Notes.Length; _dataIndex++)
            {
                var note = _data.Notes[_dataIndex];
                var go = Instantiate(_notePrefab, _container.transform.position + Vector3.right * note.Time * 10f, Quaternion.identity);
                go.transform.parent = _container.transform;
            }
        }

        public NoteData Parse(string line)
        {
            var data = line.Split(' ');
            var time = int.Parse(data[0]);
            var dir = data[1][0] switch
            {
                'U' => NoteDirection.Up,
                'D' => NoteDirection.Down,
                'L' => NoteDirection.Left,
                'R' => NoteDirection.Right,
                _ => throw new NotImplementedException()
            };
            return new()
            {
                Time = time,
                Direction = dir
            };
        }
    }
}
