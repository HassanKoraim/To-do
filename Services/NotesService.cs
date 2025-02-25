using Entities;
using Services.Helpers;
using ServicesConstract;
using ServicesConstract.DTO;

namespace Services
{
    public class NotesService : INotesService
    {
        private readonly NotesDbContext _db;
        public NoteResponse AddMission(NoteAddRequest noteAddRequest)
        {
            if(noteAddRequest == null)
                throw new ArgumentNullException(nameof(noteAddRequest));
            ValidationHelper.ModelValidation(nameof(noteAddRequest));
            Note note = noteAddRequest.ToNote();
            _db.Add(note);
            return note.ToNoteResponse();
        }

        public bool DeleteMission(int Id)
        {
            throw new NotImplementedException();
        }

        public List<NoteResponse> GetAllMissions()
        {
            throw new NotImplementedException();
        }

        public NoteResponse UpdateMission()
        {
            throw new NotImplementedException();
        }
    }
}
