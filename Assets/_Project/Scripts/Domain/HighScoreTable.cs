using System.Collections.Generic;

namespace RythmGame.Domain
{
    //This class handles the management of the High score registers
    public class HighScoreTable
    {
        //Total number of score entries to keep
        public const int MAX_ENTRIES = 3;

        //List of scores to be shown on the view
        private readonly List<int> _scores = new List<int>();

        public IReadOnlyList<int> Scores => _scores;

        //Loads the top 3 scores to be shown on the view into a list
        public void LoadScores(IEnumerable<int> scores)
        {
            _scores.Clear();
            _scores.AddRange(scores);
            _scores.Sort((a, b) => b.CompareTo(a));
            TrimToMax();
        }

        //Adds a new score only if it s greater than any of the existing scores
        public bool TryAddScore(int score)
        {
            if (_scores.Count >= MAX_ENTRIES && score <= _scores[_scores.Count - 1])
            {
                return false;
            }

            int insertIndex = _scores.FindIndex(existing => score > existing);
            if (insertIndex < 0)
            {
                _scores.Add(score);
            }
            else
            {
                _scores.Insert(insertIndex, score);
            }

            TrimToMax();
            return true;
        }

        //Makes sure only 3 scores are kept
        private void TrimToMax()
        {
            if (_scores.Count > MAX_ENTRIES)
            {
                _scores.RemoveRange(MAX_ENTRIES, _scores.Count - MAX_ENTRIES);
            }
        }
    }
}
