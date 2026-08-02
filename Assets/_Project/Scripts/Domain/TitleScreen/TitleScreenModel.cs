using System;

namespace RythmGame.Domain.TitleScreen
{
    public class TitleScreenModel
    {
        public event Action OnGameStartRequested;

        public void RequestGameStart()
        {
            OnGameStartRequested?.Invoke();
        }
    }
}