using System;

namespace RythmGame.Domain
{
    public class GameoverState : IStateMachine
    {
        public event Action OnStateComplete;

        public void Enter(double dspTime,  float offset)
        {
            // Logic for entering the Gameover state (e.g., display score)
        }

        public void Exit()
        {
            // Logic for exiting the Gameover state
        }

        public void Update(double dspTime)
        {
            // Update logic for Gameover state
        }
    }
}
