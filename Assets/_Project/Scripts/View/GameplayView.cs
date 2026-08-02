using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using RythmGame.Domain;

namespace RythmGame.View
{
    //Handles the visual elements on the screen
    public class GameplayView : MonoBehaviour
    {
        //Duration of the flashing effect
        private const float BEAT_FLASH_DURATION = 0.15f;

        //Direct reference to UI elements and objects containing view scripts
        [SerializeField] private BgView _bgView;
        [SerializeField] private TextMeshProUGUI _gameOverText;
        [SerializeField] private TextMeshProUGUI _highScoresText;
        [SerializeField] private RectTransform _canvasRectTransform;
        [SerializeField] private RectTransform _noteSpawnParent;
        [SerializeField] private NoteView _notePrefab;
        [SerializeField] private float _noteOffscreenMargin = 60f;
        [SerializeField] private float _noteLaneY;
        [SerializeField] private RectTransform _hitLine;
        [SerializeField] private Button _replayButton;

       //This handles notes spawning preparation, despawning and destruction
        private readonly NoteRegistry _noteRegistry = new NoteRegistry();

       //Properties of the flashing timing line
        private Image _hitLineImage;
        private Color _hitLineBaseColor;
        private float _hitLineFlashIntensity;

        //Sets the position the timing line
        public void SetHitLinePos(float positionX)
        {
            if (_hitLine != null)
            {
                _hitLine.anchoredPosition = new Vector2(positionX, _noteLaneY);
                _hitLineImage = _hitLine.GetComponent<Image>();
                if (_hitLineImage != null)
                {
                    _hitLineBaseColor = _hitLineImage.color;
                }
            }
        }

        //Returns Screen poperties: Width
        public float GetScreenWidth()
        {
            return _canvasRectTransform.rect.width;
        }

        //Returns Screen poperties: Height
        public float GetScreenHeight()
        {
            return _canvasRectTransform.rect.height;
        }

        //Makes the timing line flash
        private void Update()
        {
            if (_hitLineImage == null)
            {
                return;
            }

            _hitLineFlashIntensity = Mathf.Max(0f, _hitLineFlashIntensity - Time.deltaTime / BEAT_FLASH_DURATION);
            _hitLineImage.color = Color.Lerp(_hitLineBaseColor, Color.red, _hitLineFlashIntensity);
        }

        //Re-intensifies (sets Lerp max value to 1f) the timing line during each beat
        public void FlashHitLine(int beat)
        {
            _hitLineFlashIntensity = 1f;
        }

        //Hides/Shows the timing line
        public void SetHitLineVisibility(bool visibility)
        {
            _hitLine.gameObject.SetActive(visibility);
        }

        //Gathers the necessary information to spawn a note and passes this information the NoteView
        public void SpawnNote(int noteColor, float speed, int id, float timeStamp, Conductor conductor, float noteDistanceToHitPoint, float noteTravelDistancePerBeat)
        {
            if (_notePrefab == null || _noteSpawnParent == null || _canvasRectTransform == null)
            {
                return;
            }

            //Note spawning point
            float halfWidth = _canvasRectTransform.rect.width / 2f;
            float spawnX = -halfWidth - _noteOffscreenMargin;
            float despawnX = halfWidth + _noteOffscreenMargin;

           //Calls the NoteView and passes the necessary information to spawn a note prefab on the screen
            NoteView note = Instantiate(_notePrefab, _noteSpawnParent);
            note.Initialize(new Vector2(spawnX, _noteLaneY), noteColor, speed, despawnX, id, timeStamp, conductor, noteDistanceToHitPoint, noteTravelDistancePerBeat);
            _noteRegistry.Register(id, note);
        }

        //Calculates the distance the note has to travel from the spawning point to the timing line
        public float GetDistanceToHitPoint(float posX)
        {
            float halfWidth = _canvasRectTransform.rect.width / 2f;
            return (halfWidth + _noteOffscreenMargin)+posX;
        }

        public void ClearNotes()
        {
            _noteRegistry.Clear();
        }

        //Passes the arguments of the screen size to the View
        public void SetBgSize(float ratio, float y, float posy)
        {
            _bgView.SetViewBgSize(ratio, y, posy);
        }

        //Passes the id of the note to be destoyed to the view
        public void DestroyNoteWithTag(int index)
        {
            _noteRegistry.TryDestroy(index);
        }
        
        public void ShowGameOver(bool showValue)
        {
            if (_gameOverText != null)
            {
                _gameOverText.text = "G A M E  O V E R";
                _highScoresText.text = "High Scores";
                _gameOverText.gameObject.SetActive(showValue);
                _highScoresText.gameObject.SetActive(showValue);
                _replayButton.gameObject.SetActive(showValue);
            }
        }
    }
}