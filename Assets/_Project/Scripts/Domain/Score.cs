using System;

namespace RythmGame.Domain
{
    //The class contains the necessary information to handle the player score
    public class Score
    {
        private const int SCORE_VALUE = 50;

        //This is invoked when it is time to update the score on the view
        public event Action OnScoreUpdate;
        //This is invoked when it is time to update the multiplier value
        public event Action OnMultiplierUpdate;

        private int _multiplier;
        private int _scoreValue;

        public int ScoreValue
        {
            get
            {
                return _scoreValue;
            }
            set 
            {
                _scoreValue = value;
            }
        }

        public int Multiplier
        {
            get
            {
                return _multiplier;
            }
            set 
            {
                _multiplier = value;
            }
        }

        public void Initialize()
        {
            _multiplier=1;
            _scoreValue=0;
        }

        //Increased the multiplier value by one
        public void IncreaseMultiplier()
        {
            _multiplier+=1;
            OnMultiplierUpdate?.Invoke();
        }

        //Resets the multiplier back to 1
        public void ResetMultiplier()
        {
            _multiplier=1;
            OnMultiplierUpdate?.Invoke();
        }
        
        public void UpdateScore()
        {
            _scoreValue+=SCORE_VALUE*_multiplier;
            OnScoreUpdate?.Invoke();

        }

    }

}