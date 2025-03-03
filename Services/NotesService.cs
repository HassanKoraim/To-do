using Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ServiceConstracts.DTO;
using Services.Helpers;
using ServicesConstract;
using ServicesConstract.DTO;

namespace Services
{
    public class NotesService : INotesService
    {
        private readonly ApplicationDbContext _db;
        public NotesService(ApplicationDbContext notesDbContext)
        {
            _db = notesDbContext;
        }
        public NoteResponse AddNote(NoteAddRequest? noteAddRequest)
        {
            if(noteAddRequest == null)
                throw new ArgumentNullException(nameof(noteAddRequest));
            ValidationHelper.ModelValidation(noteAddRequest);
            Note note = noteAddRequest.ToNote();
            _db.Notes.Add(note);
            _db.SaveChanges();
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

        public NoteResponse GetNoteByNoteId(int? noteId)
        {
            if(noteId == null)
                throw new ArgumentNullException(nameof(noteId));
            Note? note = _db.Notes.FirstOrDefault(temp => temp.Id == noteId);
            if (note == null)
                return null;
            return note.ToNoteResponse();
        }

        public NoteResponse UpdateNote(NoteUpdateRequest? noteUpdateRequest)
        {
            if(noteUpdateRequest == null)
                throw new ArgumentNullException(nameof(noteUpdateRequest));
            ValidationHelper.ModelValidation(noteUpdateRequest);
            Note? note = _db.Notes.FirstOrDefault(temp=> temp.Id == noteUpdateRequest.Id);
            if (note == null)
                throw new ArgumentException("Given Note Id not exist");
            note.Title = noteUpdateRequest.Title;
            note.Description = noteUpdateRequest.Description;
            note.Status = noteUpdateRequest.Status;
            note.DueTime = noteUpdateRequest.DueTime;
            _db.Notes.Add(note);
            _db.SaveChanges();
            return note.ToNoteResponse();
        }
    }
}
