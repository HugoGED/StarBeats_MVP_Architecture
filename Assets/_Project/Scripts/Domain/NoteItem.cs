namespace RythmGame.Domain
{
    //This class holds the necessary information for a note to be shown on the view
    public class NoteItem
    {
        //Registered song position, in beats
        private float _itemTimeStamp;
        //Registered Note color
        private int _itemColor;
        //Registered status of the note
        private bool _itemPressed;
        //If the note has been spawned
        private bool _itemSpawned;

        public float ItemTimeStamp
        {
            get 
            { 
                return _itemTimeStamp; 
            }
            set 
            { 
                _itemTimeStamp = value;
            }
        }

        public int ItemColor
        {
            get 
            { 
                return _itemColor; 
            }
            set 
            { 
               _itemColor = value;
            }
        }

        public bool ItemPressed
        {
            get 
            { 
                return _itemPressed; 
            }
            set 
            { 
               _itemPressed = value;
            }
        }

        public bool ItemSpawned
        {
            get 
            { 
                return _itemSpawned; 
            }
            set 
            { 
               _itemSpawned = value;
            }
        }

        public NoteItem(float noteTimeStamp, int itemColor)
        {
            _itemTimeStamp = noteTimeStamp;
            _itemColor = itemColor;
            _itemPressed = false;
            _itemSpawned = false;
        }
    }
}