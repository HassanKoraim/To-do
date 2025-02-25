using Entities;
using Services.Helpers;
using ServicesConstract;
using ServicesConstract.DTO;

namespace Services
{
    public class NotesService : INotesService
    {
        private readonly NotesDbContext _db;
        public NotesService(NotesDbContext notesDbContext)
        {
            _db = notesDbContext;
        }
        public NoteResponse AddNote(NoteAddRequest? noteAddRequest)
        {
            if(noteAddRequest == null)
                throw new ArgumentNullException(nameof(noteAddRequest));
            ValidationHelper.ModelValidation(nameof(noteAddRequest));
            Note note = noteAddRequest.ToNote();
            _db.Notes.Add(note);
            return note.ToNoteResponse();
        }
        
        public bool DeleteNote(int? Id)
        {
            if(Id == null) 
                throw new ArgumentNullException("Please Given Id");
            var note = _db.Notes.FirstOrDefault(x => x.Id == Id);
            if (note == null)
                return false;
            _db.Notes.Remove(note);
            _db.SaveChanges();
            return true;
        }

        public List<NoteResponse> GetAllNotes()
        {
            List<Note> notes = _db.Notes.ToList();
            List<NoteResponse> notesResponse = notes.Select(temp => temp.ToNoteResponse()).ToList();
            return notesResponse;
        }

        public NoteResponse UpdateNote()
        {
            throw new NotImplementedException();
        }
    }
}
