using System;

//concrete implementation of the Actions Manager
namespace RythmGame.Domain
{
    public class ActionsManager : IActionsManager
    {
        public event Action<int> OnPress;

        public void PressAction(int button)
        {
            OnPress?.Invoke(button);
        }
    }
}