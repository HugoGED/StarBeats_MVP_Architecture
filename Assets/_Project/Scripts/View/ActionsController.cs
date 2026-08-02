using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace RythmGame.View
{
    //Class that holds direct references to the Unity InputSystem Actions
    public class ActionsController : MonoBehaviour
    {

        private InputSystem_Actions _inputActions;

        public event Action<int> OnArrowAction;

        void Awake()
        {
            _inputActions = new InputSystem_Actions();
        }

        void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Player.LeftArrow.started += OnLeftPressed;
            _inputActions.Player.RightArrow.started += OnRightPressed;
            _inputActions.Player.DownArrow.started += OnDownPressed;
        }

        void OnDisable()
        {
            _inputActions.Player.LeftArrow.started -= OnLeftPressed;
            _inputActions.Player.RightArrow.started -= OnRightPressed;
            _inputActions.Player.DownArrow.started -= OnDownPressed;
            _inputActions.Disable();
        }

        //Invokes the OnArrowAction passing an integer as button id
        //ID depends on the key pressed
        void OnLeftPressed(InputAction.CallbackContext callbackContext)
        {
            OnArrowAction?.Invoke(0);
        }

        void OnRightPressed(InputAction.CallbackContext callbackContext)
        {
            OnArrowAction?.Invoke(2);
        }

        void OnDownPressed(InputAction.CallbackContext callbackContext)
        {
            OnArrowAction?.Invoke(1);
        }

        void Update()
        {
        
        }
    }
}
