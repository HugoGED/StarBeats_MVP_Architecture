using UnityEngine;
using UnityEngine.UI;
using System;

namespace RythmGame.View
{
    //Controls the scene navigations with button listeners
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        public event Action OnStartButtonClicked;

        void Awake()
        {
            _startButton.onClick.AddListener(HandleStartButtonClick);
        }

        private void HandleStartButtonClick()
        {
            OnStartButtonClicked?.Invoke();
        }

        void OnDestroy()
        {
            _startButton.onClick.RemoveListener(HandleStartButtonClick);
        }
    }
}