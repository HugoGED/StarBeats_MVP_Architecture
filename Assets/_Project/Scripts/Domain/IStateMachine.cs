using System;

namespace RythmGame.Domain
{
    public interface IStateMachine
    {
        void Enter(double dspTime,  float offset);
        void Exit();
        void Update(double dspTime);
        event Action OnStateComplete;
    }
}
