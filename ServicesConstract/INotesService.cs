
using Entities;
using ServiceConstracts.DTO;
using ServicesConstract.DTO;

namespace ServicesConstract
{
    public interface INotesService
    {
        List<NoteResponse> GetAllNotes();
        NoteResponse AddNote(NoteAddRequest? noteAddRequest);
        NoteResponse UpdateNote(NoteUpdateRequest? noteUpdateRequest);
        NoteResponse GetNoteByNoteId(int? noteId);
        bool DeleteNote(int? Id);
    }
}
