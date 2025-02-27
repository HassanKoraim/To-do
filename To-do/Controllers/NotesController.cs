using Microsoft.AspNetCore.Mvc;
using ServiceConstracts.DTO;
using ServicesConstract;
using ServicesConstract.DTO;

namespace To_do.Controllers
{
    [Route("[controller]")]
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

        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Create(NoteAddRequest? noteAddRequest)
        {
            _notesService.AddNote(noteAddRequest);
            return RedirectToAction("Index");
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Update(int? id)
        {
            NoteResponse noteResponse = _notesService.GetNoteByNoteId(id);
            return View(noteResponse);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Update(NoteUpdateRequest noteUpdateRequest)
        {
            _notesService.UpdateNote(noteUpdateRequest);
            return RedirectToAction("Index", "Notes");
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            _notesService.DeleteNote(id);
            return RedirectToAction("Index");
        }
    }
}
