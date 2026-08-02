using RythmGame.View;
using RythmGame.Domain;

namespace RythmGame.Presenter
{
    //Connects the events defined in the Domain to the specific methods to handle input in the ActionsController in the View
    public class ActionManagerPresenter
    {
        private ActionsManager _actionsManager;
        private ActionsController _actionsController;

        public ActionManagerPresenter(ActionsManager actionsManager, ActionsController ActionsController)
        {
            _actionsManager = actionsManager;
            _actionsController = ActionsController;
            _actionsController.OnArrowAction += OnArrowAction;
        }

        public void OnArrowAction(int button)
        {
            _actionsManager.PressAction(button);

        }

        public void Dispose()
        {
            _actionsController.OnArrowAction -= OnArrowAction;
        }

    }
}