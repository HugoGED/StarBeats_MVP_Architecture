using UnityEngine;
using RythmGame.Domain.TitleScreen;
using RythmGame.View.TitleScreen;

namespace RythmGame.Presenter.TitleScreen
{
    public class TitleScreenPresenter : MonoBehaviour
    {
        [SerializeField] private TitleScreenView _view;
        private TitleScreenModel _model;

        private const string GAME_TITLE = "S T A R \nBEATS";

        //Sets the game title text and adds the start button listener
        private void Awake()
        {
            _model = new TitleScreenModel();
            _view.SetGameTitle(GAME_TITLE);
            _view.SetBgSize(2f, _view.GetScreenHeight()+100f, -50f);
            _view.AddStartButtonListener(_model.RequestGameStart);
        }

        private void OnDestroy()
        {
            _view.RemoveStartButtonListener(_model.RequestGameStart);
        }
    }
}