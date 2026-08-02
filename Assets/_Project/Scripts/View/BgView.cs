using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RythmGame.View
{
    //Handles the background positioning ans scale on the screen
    public class BgView : MonoBehaviour
    {
        [SerializeField] Image _background;

        public void SetViewBgSize(float ratio, float y, float posy)
        {
            _background.rectTransform.anchoredPosition = new Vector2(0f, posy);
            _background.rectTransform.sizeDelta = new Vector2(y*ratio, y);
        }
    }
}