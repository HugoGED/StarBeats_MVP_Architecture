using System;

namespace RythmGame.Domain
{
    public interface IActionsManager
    {
        event Action<int> OnPress;
        void PressAction(int button);
    }
}