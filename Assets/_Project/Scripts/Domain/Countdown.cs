using System;

namespace RythmGame.Domain
{
    //This class handles the inital countdown when the game starts
    //Currently disabled
    public class Countdown
    {
        //Countdown start number
        private const int START_COUNT = 3;

        public event Action<int> OnCountChanged;
        public event Action OnCountdownComplete;

        private readonly Conductor _conductor;
        private double _startDspTime;
        private double _countTime;
        private float _noteSpeed;
        private bool _isComplete;
        private float _timeToHitPoint;
        private int _count;

        public float TimeToHitPoint => _timeToHitPoint;

        public Countdown(Conductor conductor, float speed, float distance)
        {
            _conductor = conductor;
            _timeToHitPoint = distance/speed;
        }

        //Set the initial values before the countdown starts
        public void Start(double dspTime)
        {
            _startDspTime = dspTime;
            _isComplete = false;
            _count = START_COUNT;
            OnCountChanged?.Invoke(_count);
            _countTime = dspTime;
            Update(dspTime);
        }

        public void Update(double dspTime)
        {
            if (_isComplete)
            {
                return;
            }

            //Verifies if the necessary time has passed for the countdown to progress
            if((dspTime - _countTime)>=(_timeToHitPoint/START_COUNT))
            {
                _countTime=dspTime;
                _count-=1;
                OnCountChanged?.Invoke(_count);
            }

             //Verifies if the time to reach the first note has been reached, thus the countdown is over
            if((dspTime - _startDspTime)>=_timeToHitPoint)
            {
                _isComplete = true;
                OnCountdownComplete?.Invoke();
            }
        }
    }
}
