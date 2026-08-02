using System.Collections.Generic;
using UnityEngine;

namespace RythmGame.View
{
    //Handles Note (prefab) despawning and disposing
    public class NoteRegistry
    {
        private readonly Dictionary<int, NoteView> _notesById = new Dictionary<int, NoteView>();

        //adds a noteView element to the dictionary to despawn later based on id
        public void Register(int id, NoteView noteView)
        {
            _notesById[id] = noteView;
            noteView.Despawned += HandleDespawned;
        }

       //Tries destroying a noteView object
        public bool TryDestroy(int id)
        {
            if (!_notesById.TryGetValue(id, out NoteView noteView))
            {
                return false;
            }

            Object.Destroy(noteView.gameObject);
            return true;
        }

        public void Clear()
        {
            foreach (NoteView noteView in _notesById.Values)
            {
                if (noteView != null)
                {
                    Object.Destroy(noteView.gameObject);
                }
            }

            _notesById.Clear();
        }

        private void HandleDespawned(int id)
        {
            _notesById.Remove(id);
        }
    }
}
