using System;
using UnityEngine;
using UnityEngine.UI;
using RythmGame.Domain;

namespace RythmGame.View
{
    //Class that hold the visual representation of a note
    public class NoteView : MonoBehaviour
    {
        //Array of possible color notes
        private static readonly Color[] NOTE_COLORS = { Color.red, Color.blue, Color.green };

        //References to the actual image objects of each note color
        [SerializeField] private Image _redBottle;
        [SerializeField] private Image _blueBottle;
        [SerializeField] private Image _greenBottle;

        //Time and space information to draw and move the note accordingly
        private RectTransform _rectTransform;
        private float _speed;
        private float _despawnPositionX;
        private float _spawnPositionX;
        private float _rotation;
        private int _id;
        private float _timeStamp;
        private float _radialSpeed;
        private float _noteTravelDistancePerBeat;
        private Conductor _conductor;

        public event Action<int> Despawned;

        //A generic note prefab does not know what color it is yet
        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
            _redBottle.gameObject.SetActive(false);
            _blueBottle.gameObject.SetActive(false);
            _greenBottle.gameObject.SetActive(false);
        }

        //Assigns the respective color, position and speed to the note
        public void Initialize(Vector2 spawnPosition, int noteColor, float speed, float despawnPositionX, int id, float timeStamp, Conductor conductor, float noteDistanceToHitPoint, float noteTravelDistancePerBeat)
        {
            _rectTransform.anchoredPosition = spawnPosition;
            _speed = speed;
            _timeStamp=timeStamp;
            _conductor = conductor;
            _noteTravelDistancePerBeat = noteTravelDistancePerBeat;
            _spawnPositionX = spawnPosition.x;
            _despawnPositionX = despawnPositionX;
            _id = id;

            switch (noteColor)
                {
                    case 0:
                        _redBottle.gameObject.SetActive(true);
                        break;
                    case 1:
                        _blueBottle.gameObject.SetActive(true);
                        break;
                    case 2:
                        _greenBottle.gameObject.SetActive(true);
                        break;
                }
        }

        //Moves the note by changing its position
        private void Update()
        {
            Vector2 position = _rectTransform.anchoredPosition;
            position.x= _spawnPositionX + (_speed*(_conductor.songPosition-_timeStamp));
            
            _rectTransform.anchoredPosition = position;

            //A note will be despawned when it reaches the right screen border if it was not hit
            if (position.x >= _despawnPositionX)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            Despawned?.Invoke(_id);
        }
    }
}
