using UnityEngine;
using RythmGame.View;
using RythmGame.Domain;

namespace RythmGame.Presenter
{
    //Holds the references to the Scene Loader (Domain) and Menu Presenter (presenter)
    public class MenuBootstrapper : MonoBehaviour
    {
        [SerializeField] private MenuController _menuController;

        private MenuPresenter _menuPresenter;
        private SceneLoader _sceneLoader;

        void Awake()
        {
            _sceneLoader = new SceneLoader();
            _menuPresenter = new MenuPresenter(_menuController, _sceneLoader);
        }

        void OnDestroy()
        {
            _menuPresenter?.Dispose();
        }
    }
}