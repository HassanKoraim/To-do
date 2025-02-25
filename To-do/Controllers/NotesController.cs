using Microsoft.AspNetCore.Mvc;
using ServicesConstract;
using ServicesConstract.DTO;

namespace To_do.Controllers
{
    [Controller]
    public class NotesController : Controller
    {
        private readonly INotesService _notesService;
        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            List<NoteResponse> notesResponse = _notesService.GetAllNotes();
            return View(notesResponse);
        }
    }
}
