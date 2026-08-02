using UnityEngine;
using RythmGame.Domain;
using RythmGame.View;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RythmGame.Presenter
{
    //This handles the main game logic using the necessary classes from the Domain and updating the elements in the view
    public class GameplayPresenter : MonoBehaviour
    {
        private const float POS_X = 193;

        [SerializeField] private GameplayView _gameplayView;
        [SerializeField] private CountdownView _countdownView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private HighScoreView _highScoreView;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private ActionsBootstrapper _actionsBootstrapper;

        //Song information
        [SerializeField] private float _songBpm;
        [SerializeField] private float _noteTravelDistancePerBeat;
        [SerializeField] private float _SongFirstBeatOffset;

        //Domain classes
        private IStateMachine _currentState;
        private Conductor _conductor;
        private NoteSpawner _noteSpawner;
        private IActionsManager _actionsManager;
        private Countdown _countdown;
        private Score _scoreManager;
        private HighScoreTable _highScoreTable;
        
        private HighScoreStorage _highScoreStorage;

        //Game states
        private IntroState _introState;
        private PlayingState _playingState;
        private GameoverState _gameoverState;

        //Information related to notes movement
        private float _noteSpeed;
        private float _noteDistanceToHitPoint;

        private void Awake()
        {
            //Gameplay framerate
            Application.targetFrameRate = 60;

            //Positions the hit timing line on the X-axis
            _gameplayView.SetHitLinePos(POS_X);

            //Create the conductor
            _conductor = new Conductor();
            _conductor.Initialize(_songBpm, AudioSettings.dspTime, _SongFirstBeatOffset);
            _conductor.OnBeat += _gameplayView.FlashHitLine;

            //Create the note spawner
            _noteSpawner = new NoteSpawner(_conductor);
            _noteSpawner.OnNoteSpawned += HandleNoteSpawned;
            _noteSpawner.OnPressMissed += HandlePressMissed;

            //Get the necessary information to calculate note movement
            _noteSpeed = _noteTravelDistancePerBeat / _conductor.secPerBeat;
            _noteDistanceToHitPoint = _gameplayView.GetDistanceToHitPoint(POS_X);

            //Create the countdown
            _countdown = new Countdown(_conductor, _noteSpeed, _noteDistanceToHitPoint);
            _countdown.OnCountChanged += _countdownView.SetCount;
            _countdown.OnCountdownComplete += () => ChangeState(_playingState);

            //Create a score object
            _scoreManager = new Score();
            _scoreManager.Initialize();

            //Create the necessary variables to handle score storage
            _highScoreStorage = new HighScoreStorage();
            _highScoreTable = new HighScoreTable();
            _highScoreTable.LoadScores(_highScoreStorage.LoadScores());

            //Create the game states
            _introState = new IntroState(_conductor);
            _playingState = new PlayingState(_conductor);
            _gameoverState = new GameoverState();

            _playingState.OnStateComplete += () => ChangeState(_gameoverState);
        }

        private void Start()
        {
            //Register events
            _actionsManager = _actionsBootstrapper.ActionsManager;
            _actionsManager.OnPress  += HandlePress;
            _scoreManager.OnScoreUpdate += UpdateScore;
            _scoreManager.OnMultiplierUpdate += UpdateMultiplier;

            //Game starts in the intro state
            ChangeState(_introState);
        }

        //Update the Note Spawner based on the current state
        private void Update()
        {
            _currentState?.Update(AudioSettings.dspTime);
            if (_currentState == _introState)
            {
                _noteSpawner.Update(_conductor.songPosition, false);
                _countdown.Update(AudioSettings.dspTime);
            }

            if (_currentState == _playingState)
            {
                _noteSpawner.Update(_conductor.songPosition, true);
            }
        }

        //Handles state change
        public void ChangeState(IStateMachine newState)
        {
            var oldState = _currentState;
            oldState?.Exit();

            //The game finished, the audio is stopped and the notes on screen cleared
            if (oldState == _playingState)
            {
                _audioSource.Stop();
                _gameplayView.ClearNotes();
            }
               
            //Updates the view elements based on the current state
            _currentState = newState;
            _countdownView.SetVisible(false);
            _scoreView.SetVisible(newState == _playingState);
            _gameplayView.SetHitLineVisibility(newState != _gameoverState);
            _highScoreView.SetVisible(newState == _gameoverState);

            double dspTime = AudioSettings.dspTime;

            if (newState == _introState)
            {
                _gameplayView.SetBgSize(2f, _gameplayView.GetScreenHeight()+100f, -50f);
                _gameplayView.ShowGameOver(false);
                _noteSpawner.Reset();
                _noteSpawner.GenerateNotesQueue();

                //The song is actually started during the intro state but in silence
                //This is to calculate the BPM and set the game speed
                _audioSource.mute=true;
                _audioSource.Play();

                _countdown.Start(dspTime);
            }
            else if (newState == _playingState)
            {
                //The song is re-started and unmuted
                //The actual song time value will not be restarted so an offset is created here to verify the hits in the future
                _audioSource.time = 0f; 
                _audioSource.mute=false;
                _conductor.SetOffset(_countdown.TimeToHitPoint);
            }
            else if (newState == _gameoverState)
            {
                //The game is over and the scores are registered
                _gameplayView.ShowGameOver(true);

                _highScoreTable.TryAddScore(_scoreManager.ScoreValue);
                _highScoreStorage.SaveScores(_highScoreTable.Scores);
                _highScoreView.SetScores(_highScoreTable.Scores);
            }
            _currentState.Enter(dspTime,_SongFirstBeatOffset);
        }
        
        private void HandleNoteSpawned(Note note)
        {
            _gameplayView.SpawnNote(note.noteColor, _noteSpeed, note.noteId, note.noteTimeStamp, _conductor, _noteDistanceToHitPoint, _noteTravelDistancePerBeat);
        }

        private void HandlePressMissed()
        {
            _scoreManager.ResetMultiplier();
        }

        //Handles the button press event and calls the function in charge of evaluating the hit logic (EvaluateHit)
        private void HandlePress(int button)
        {
            int hitId;

            if (_currentState == _playingState){
                hitId = _noteSpawner.EvaluateHit(_conductor.songPosition-_conductor.offset,button);
            }
            else{hitId=-1;}

            if (hitId>=0)
            {
                _scoreManager.UpdateScore();
                _scoreManager.IncreaseMultiplier();
                _gameplayView.DestroyNoteWithTag(hitId);

            }
            else{
                _scoreManager.ResetMultiplier();
                }
        }

        //Updates the score in the view side
        private void UpdateScore()
        {
            _scoreView.SetScore(_scoreManager.ScoreValue);
        }

       //Updates the multiplier counter in the view side
        private void UpdateMultiplier()
        {
            _scoreView.SetMultiplier(_scoreManager.Multiplier);
        }

        private void ReloadScene()
        {

        }

        private void OnDestroy()
        {
            _actionsManager.OnPress  -= HandlePress;
            _noteSpawner.OnNoteSpawned -= HandleNoteSpawned;
            _noteSpawner.OnPressMissed -= HandlePressMissed;
            _conductor.OnBeat -= _gameplayView.FlashHitLine;
            _countdown.OnCountChanged -= _countdownView.SetCount;
            _countdown.OnCountdownComplete -= () => ChangeState(_playingState);
        }
    }
}
