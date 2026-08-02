using System.Collections.Generic;
using UnityEngine;

namespace RythmGame.Presenter
{
    //Handles score saving using the unity player prefs
    public class HighScoreStorage
    {
        private const string KEY_PREFIX = "HighScore_";
        private const int MAX_ENTRIES = 3;

        //Returns the scores stored in the unity player prefs
        public int[] LoadScores()
        {
            int[] scores = new int[MAX_ENTRIES];
            for (int i = 0; i < MAX_ENTRIES; i++)
            {
                scores[i] = PlayerPrefs.GetInt(KEY_PREFIX + i, 0);
            }

            return scores;
        }

        //Saves the scores in the player prefs
        public void SaveScores(IReadOnlyList<int> scores)
        {
            for (int i = 0; i < MAX_ENTRIES; i++)
            {
                int value = i < scores.Count ? scores[i] : 0;
                PlayerPrefs.SetInt(KEY_PREFIX + i, value);
            }

            PlayerPrefs.Save();
        }
    }
}
