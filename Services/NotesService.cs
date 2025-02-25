using Entities;
using Services.Helpers;
using ServicesConstract;
using ServicesConstract.DTO;

namespace Services
{
    public class NotesService : INotesService
    {
        private readonly NotesDbContext _db;
        public NoteResponse AddNote(NoteAddRequest noteAddRequest)
        {
            if(noteAddRequest == null)
                throw new ArgumentNullException(nameof(noteAddRequest));
            ValidationHelper.ModelValidation(nameof(noteAddRequest));
            Note note = noteAddRequest.ToNote();
            _db.Add(note);
            return note.ToNoteResponse();
        }

        public bool DeleteNote(int Id)
        {
            throw new NotImplementedException();
        }

        public List<NoteResponse> GetAllNotes()
        {
            throw new NotImplementedException();
        }

        public NoteResponse UpdateNote()
        {
            throw new NotImplementedException();
        }
    }
}
