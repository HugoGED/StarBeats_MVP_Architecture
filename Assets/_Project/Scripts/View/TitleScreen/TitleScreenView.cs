using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RythmGame.View.TitleScreen
{
    public class TitleScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gameTitleText;
        [SerializeField] private Button _startButton;
        [SerializeField] private Image _background;
        [SerializeField] private RectTransform _canvasRectTransform;

        public void SetGameTitle(string title)
        {
            if (_gameTitleText != null)
            {
                _gameTitleText.text = title;
            }
        }

         public float GetScreenHeight()
        {
            return _canvasRectTransform.rect.height;
        }

        public void SetBgSize(float ratio, float y, float posy)
        {
            _background.rectTransform.anchoredPosition = new Vector2(0f, posy);
            _background.rectTransform.sizeDelta = new Vector2(y*ratio, y);
        }

        public void AddStartButtonListener(UnityEngine.Events.UnityAction action)
        {
            _startButton.onClick.AddListener(action);
        }

        public void RemoveStartButtonListener(UnityEngine.Events.UnityAction action)
        {
            _startButton.onClick.RemoveListener(action);
        }
    }
}