namespace RythmGame.Domain
{
    // This is the note class, a note contains the following information: color, id, time stamp
    public class Note
    {
        public int noteColor;
        public int noteId;
        public float noteTimeStamp;

        public Note(int noteColorValue, int noteIdentifier, float noteTimeStampValue)
        {
            this.noteColor = noteColorValue;
            this.noteId = noteIdentifier;
            this.noteTimeStamp = noteTimeStampValue;
        }
    }
}