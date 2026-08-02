using UnityEngine;
using RythmGame.View;
using RythmGame.Domain;

namespace RythmGame.Presenter
{
    //Holds the references to the actions manager (view) and actions manager presenter (presenter) used by the Gameplay Presenter
    public class ActionsBootstrapper : MonoBehaviour
    {
        [SerializeField] private ActionsController _actionsController;

        private ActionManagerPresenter _actionManagerPresenter;
        private ActionsManager _actionsManager;
        public IActionsManager ActionsManager { get { return _actionsManager; } }

        void Awake()
        {
            _actionsManager = new ActionsManager();
            _actionManagerPresenter = new ActionManagerPresenter(_actionsManager, _actionsController);
        }

        void OnDestroy()
        {
            _actionManagerPresenter?.Dispose();
        }
    }
}