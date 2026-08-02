using RythmGame.View;
using RythmGame.Domain;
using UnityEngine.SceneManagement;

namespace RythmGame.Presenter
{
    //This class handles scene management
    public class MenuPresenter
    {
        private MenuController _menuController;
        private SceneLoader _sceneLoader;

        public MenuPresenter(MenuController menuController, SceneLoader sceneLoader)
        {
            _menuController = menuController;
            _sceneLoader = sceneLoader;

            _menuController.OnStartButtonClicked += OnStartButtonClicked;
            _sceneLoader.OnGameplaySceneRequested += LoadGameplayScene;
        }

        private void OnStartButtonClicked()
        {
            _sceneLoader.RequestGameplayScene();
        }

        private void LoadGameplayScene()
        {
            SceneManager.LoadScene("GameplayScene");
        }

        // Call this method when the menu presenter is no longer needed
        public void Dispose()
        {
            _menuController.OnStartButtonClicked -= OnStartButtonClicked;
            _sceneLoader.OnGameplaySceneRequested -= LoadGameplayScene;
        }
    }
}