using System;
using System.Collections.Generic;

namespace RythmGame.Domain
{
    //This class manages note spawning
    public class NoteSpawner
    {
        //The probability of a note to spawn during a beat
        private const float SPAWN_PROBABILITY = 0.8f;
        //Total number of colors
        private const int NOTE_COLOR_COUNT = 3;
        //Duration of the actual game in seconds, regardless of song duration
        private const float TOTAL_GAME_DURATION = 30.0f;
        //The hit frame tolerance in seconds
        private const float TIMING_WINDOW = 0.1f;
        
        //Will be invoked when is time to spawn a note on the view
        public event Action<Note> OnNoteSpawned;
        //Will be invoked when the incorrect note is pressed
        public event Action OnPressMissed;

        private readonly Random _random;
        
        private Conductor _conductor;
        //List of total notes
        private readonly List<NoteItem> _notesList;

        //Total number of notes to be generated
        private int _totalNotes;

        public NoteSpawner(Conductor conductor, Random random = null)
        {
            _random = random ?? new Random();
            _conductor = conductor;
            _totalNotes = (int)MathF.Round(TOTAL_GAME_DURATION/_conductor.secPerBeat);
            _notesList = new List<NoteItem>();
        }

        //Clears the notes list
        public void Reset()
        {
            _notesList.Clear();
        }
        
        //Generates the total necessary notes before the game starts
        public void GenerateNotesQueue()
        {
            int color;

            for(int i=0;i<_totalNotes;i++)
            {
                //Checks the probability of spawning a note during that beat
                //Sets color=NOTE_COLOR_COUNT if not spawning, sets a random color<NOTE_COLOR_COUNT if spawning
                if ((_random.NextDouble() > SPAWN_PROBABILITY)&&(i>0))
                {
                    color=NOTE_COLOR_COUNT;
                }
                else{
                    color = _random.Next(NOTE_COLOR_COUNT);
                }
                _notesList.Add(new NoteItem(i*_conductor.secPerBeat,color));
            }
        }

        //Verifies if the note was correctly hit
        public int EvaluateHit(float songPosition, int button)
        {
            int noteId = -1;

            for(int i=0;i<_notesList.Count;i++)
            {
                //Searches for the note that matches the current time stamp
                if((songPosition>=(_notesList[i].ItemTimeStamp-TIMING_WINDOW))&&
                   (songPosition<=((_notesList[i].ItemTimeStamp-TIMING_WINDOW)+2*TIMING_WINDOW))
                ){
                    //Verifies if no button has been pressed for the current note and if the pressed color matches the note
                    if((!_notesList[i].ItemPressed)&&
                      (_notesList[i].ItemColor==button)
                    )
                    {
                        noteId=i;
                    }
                    _notesList[i].ItemPressed=true;
                }
            }
            return noteId;
        }

        //Responsible of spawning notes
        public void Update(float songPosition, bool nowPlaying)
        {
            float currentSongTime = songPosition;

            for(int i=0;
                i<_notesList.Count;
                i++)
            {
                //Handles note spawning by constatly checking the time stamps in the note list
                if((!_notesList[i].ItemSpawned)&&(currentSongTime >= _notesList[i].ItemTimeStamp))
                {
                 if(_notesList[i].ItemColor<NOTE_COLOR_COUNT)
                 {
                    OnNoteSpawned?.Invoke(new Note(_notesList[i].ItemColor,i, _notesList[i].ItemTimeStamp));
                 }
                 _notesList[i].ItemSpawned=true;
                }

                //Verifies if a note's time Stamp has passed and no button was pressed to register a miss
                if((nowPlaying)&&
                   ((currentSongTime-_conductor.offset)>((_notesList[i].ItemTimeStamp-TIMING_WINDOW)+2*TIMING_WINDOW))&&
                   (_notesList[i].ItemColor<NOTE_COLOR_COUNT)&&
                   (!_notesList[i].ItemPressed))
                {
                 _notesList[i].ItemPressed=true;
                 OnPressMissed?.Invoke();
                }
            }
        }
    }
}
