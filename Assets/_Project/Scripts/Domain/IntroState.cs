using System;

namespace RythmGame.Domain
{
    public class IntroState : IStateMachine
    {
        public event Action OnStateComplete;
        private readonly Conductor _conductor;

        public IntroState(Conductor conductor)
        {
            _conductor = conductor;
        }


        public void Enter(double dspTime, float offset)
        {
            
        }

        public void Exit()
        {
            // Logic for exiting the Intro state
        }

        public void Update(double dspTime)
        {
            _conductor.Update((float)dspTime);
        }
    }
}

