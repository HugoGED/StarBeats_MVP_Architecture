using UnityEngine;
using TMPro;

namespace RythmGame.View
{
    //Handles the countdown display on the screen
    //Currently disabled
    public class CountdownView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countdownText;

        public void SetCount(int count)
        {
            if (_countdownText == null)
            {
                return;
            }

            _countdownText.text = count.ToString();
        }

        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}
