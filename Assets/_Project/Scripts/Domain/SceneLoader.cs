using System;

namespace RythmGame.Domain
{
    public class SceneLoader
    {
        public event Action OnGameplaySceneRequested;

        public void RequestGameplayScene()
        {
            OnGameplaySceneRequested?.Invoke();
        }
    }
}