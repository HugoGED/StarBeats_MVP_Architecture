using UnityEngine;
using TMPro;

namespace RythmGame.View
{
    //Handles the score and multiplier text on the screen
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _multiplierText;
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        public void SetScore(int count)
        {
            if (_scoreText == null)
            {
                return;
            }

            _scoreText.text = "Score: "+count.ToString();
        }

        public void SetMultiplier(int count)
        {
            if (_multiplierText == null)
            {
                return;
            }

            _multiplierText.text = "X "+count.ToString();
        }

        public void SetVisible(bool visible)
        {
            _scoreText.gameObject.SetActive(visible);
            _multiplierText.gameObject.SetActive(visible);
        }
        
    }
}