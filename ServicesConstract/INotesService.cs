
using Entities;
using ServicesConstract.DTO;

namespace ServicesConstract
{
    public interface INotesService
    {
        List<NoteResponse> GetAllNotes();
        NoteResponse AddNote(NoteAddRequest noteAddRequest);
        NoteResponse UpdateNote();
        bool DeleteNote(int Id);
    }
}
