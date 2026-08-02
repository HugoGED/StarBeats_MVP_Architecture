using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RythmGame.View
{
    //Handles the display of the high scores
    public class HighScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _scoreTexts;

        //Loops the score list and assembles the string containing the score text
        public void SetScores(IReadOnlyList<int> scores)
        {
            for (int i = 0; i < _scoreTexts.Length; i++)
            {
                if (_scoreTexts[i] == null)
                {
                    continue;
                }

                int value = i < scores.Count ? scores[i] : 0;
                _scoreTexts[i].text = ""+value;
            }
        }

        //Shows/Hides the score text
        public void SetVisible(bool visible)
        {
            for (int i = 0; i < _scoreTexts.Length; i++)
            {
                if (_scoreTexts[i] == null)
                {
                    continue;
                }
                _scoreTexts[i].gameObject.SetActive(visible);
            }
        }
    }
}
