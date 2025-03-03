using ServicesConstract;
using Services;
using ServicesConstract.DTO;
using EntityFrameworkCoreMock;
using Moq;
using Entities;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using AutoFixture;
using AutoFixture.Kernel;
using System.ComponentModel.DataAnnotations;

namespace To_doTests
{
    public class NotesServiceTest
    {
        private readonly INotesService _notesService;
        private readonly IFixture _fixture;
        public NotesServiceTest()
        {
            var notesInitialData = new List<Note>() { };
            DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(
                new DbContextOptionsBuilder<ApplicationDbContext>().Options
             );
            ApplicationDbContext dbContext = dbContextMock.Object;
            dbContextMock.CreateDbSetMock(temp => temp.Notes, notesInitialData);
            _notesService = new NotesService(dbContext);
            _fixture = new Fixture();
        }

        #region AddNote
        [Fact]
        public void AddNote_NullNoteAddRequest()
        {
            //Arrange
            NoteAddRequest? noteAddRequest = null;

            //Act 
            var action = () => _notesService.AddNote(noteAddRequest);

            // Assert
            action.Should().Throw<ArgumentNullException>();

        }

        [Fact]
        public void AddNote_WithoutTitle()
        {
            // Arrange
            NoteAddRequest? noteAddRequest = _fixture.Build<NoteAddRequest>()
                .With(temp => temp.Title, null as string)
                .Create();
            //Act
            Action action = () => _notesService.AddNote(noteAddRequest);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void AddNote_WithoutDueTime()
        {
            // Arrange
            NoteAddRequest? noteAddRequest = _fixture.Build<NoteAddRequest>()
                .With(temp => temp.DueTime, (DateTime?) null)
                .Create();
            //Act
            Action action = () => _notesService.AddNote(noteAddRequest);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void AddNote_ProperDetails()
        {
            //Arrange
            NoteAddRequest noteAddRequest = _fixture.Create<NoteAddRequest>();

            // Act
            NoteResponse noteResponse = _notesService.AddNote(noteAddRequest);

            noteResponse.Should().NotBeNull(); // First Test
            Action action = () => _notesService.GetNoteByNoteId(noteResponse.Id);
           action.Should().NotBeNull(); // Second Test
            List<NoteResponse> notesResponse_list_from_get = _notesService.GetAllNotes();
            notesResponse_list_from_get.Should().ContainEquivalentOf(noteResponse); //Third Test

        }
        #endregion 


    }
}