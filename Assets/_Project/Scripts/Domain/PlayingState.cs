using System;

namespace RythmGame.Domain
{
    public class PlayingState : IStateMachine
    {
        private const float SONG_END_POSITION_SECONDS = 30f;

        public event Action OnStateComplete;
        private readonly Conductor _conductor;

        public PlayingState(Conductor conductor)
        {
            _conductor = conductor;
        }

        public void Enter(double dspTime,  float offset)
        {

        }

        public void Exit()
        {
            // Logic for exiting the Playing state
        }

        public void Update(double dspTime)
        {
            _conductor.Update((float)dspTime);

            //Verifies if the game has finished
            if (_conductor.songPosition >= (SONG_END_POSITION_SECONDS+_conductor.offset))
            {
                // Song has reached its end position, complete the state.
                OnStateComplete?.Invoke();
            }
        }
    }
}
