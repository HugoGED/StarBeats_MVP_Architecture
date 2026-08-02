using System;

namespace RythmGame.Domain
{
    //This class calculates song position in time (seconds) and bpm values based on the track
    public class Conductor
    {
        //The offset to the first beat of the song in seconds
        public float firstBeatOffset;
        //Song beats per minute determined by the song you're trying to sync up to
        public float songBpm;
        //The number of seconds for each song beat
        public float secPerBeat;
        //Current song position, in seconds
        public float songPosition;
        //Current song position, in beats
        public int songPositionInBeats;
        //How many seconds have passed since the song started
        public float dspSongTime; // This will be set by the Presenter at song start
        //Seconds a note takes to travel from its spawn point to the timing line
        public float offset;

        public event Action<int> OnBeat;

        private int _lastBeat;

        public void Initialize(float bpm, double audioDspTime, float songFirstBeatOffset)
        {
            songBpm = bpm;
            firstBeatOffset = songFirstBeatOffset;
            secPerBeat = 60f / songBpm;
            dspSongTime = (float)audioDspTime; // Record the time when the music starts from AudioSettings.dspTime
            songPosition = 0;
            songPositionInBeats = 0;
            _lastBeat = int.MinValue;
        }

        public void Update(float currentAudioTime)
        {
            // Determine how many seconds since the song started
            songPosition = (float)(currentAudioTime - dspSongTime - firstBeatOffset);
            // Determine how many beats since the song started
            songPositionInBeats = (int)MathF.Round(songPosition / secPerBeat);

            int beat = (int)MathF.Floor(songPosition / secPerBeat);
            if (beat != _lastBeat)
            {
                _lastBeat = beat;
                OnBeat?.Invoke(beat);
            }
        }

        public void SetOffset(float timeToHitPoint)
        {
            offset=timeToHitPoint;
        }
    }
}