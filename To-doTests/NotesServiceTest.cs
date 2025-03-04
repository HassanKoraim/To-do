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
using ServiceConstracts.DTO;

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

        #region GetAllNotes

        [Fact]
        public void GetAllNotes_EmptyList()
        {
            //Arrange 
            List<NoteResponse>? notesResponse_from_get = _notesService.GetAllNotes();

            // Assert
            notesResponse_from_get.Should().BeNullOrEmpty();
        }

        [Fact]
        public void GetAllNotes_AddFewNotes()
        {
            //Arrange 
            NoteAddRequest noteAddRequest1 = _fixture.Create<NoteAddRequest>();
            NoteAddRequest noteAddRequest2 = _fixture.Create<NoteAddRequest>();
            NoteAddRequest noteAddRequest3 = _fixture.Create<NoteAddRequest>();
            NoteAddRequest noteAddRequest4 = _fixture.Create<NoteAddRequest>();
            NoteResponse noteResponse1 = _notesService.AddNote(noteAddRequest1);
            NoteResponse noteResponse2 = _notesService.AddNote(noteAddRequest2);
            NoteResponse noteResponse3 = _notesService.AddNote(noteAddRequest3);
            NoteResponse noteResponse4 = _notesService.AddNote(noteAddRequest4);
            List<NoteResponse> notesResponse_list = 
                new List<NoteResponse> { noteResponse1, noteResponse2, noteResponse3, noteResponse4};
            // Act
            List<NoteResponse>? notesResponse_list_from_get = _notesService.GetAllNotes();

            //Assert
            notesResponse_list_from_get.Should().BeEquivalentTo(notesResponse_list);
        }
        #endregion

        #region GetNoteByNoteId

        [Fact]
        public void GetNoteByNoteId_IdIsNull()
        {
            // Arrange
            Guid? Id = null;

            //Act
            Action action = () => _notesService.GetNoteByNoteId(Id);

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetNoteByNoteId_IdIsNotRight()
        {
            // Arrange
            Guid? Id = Guid.NewGuid();

            //Act
            NoteResponse noteResponse = _notesService.GetNoteByNoteId(Id);

            //Assert
            noteResponse.Should().BeNull();
        }

        [Fact]
        public void GetNoteByNoteId_ProperDetails()
        {
            // Arrange
            NoteAddRequest noteAddRequest = _fixture.Create<NoteAddRequest>();
            NoteResponse noteResponse = _notesService.AddNote(noteAddRequest);

            //Act
            NoteResponse noteResponse_from_get = _notesService.GetNoteByNoteId(noteResponse.Id);

            //Assert
            noteResponse_from_get.Should().BeEquivalentTo(noteResponse);
        }
        #endregion

        #region UpdateNote
        [Fact]
        public void UpdateNote_NullNoteUpdateRequest()
        {
            // Arrrange
            NoteUpdateRequest? noteUpdateRequest = null;

            //Act 
            Action action = () => _notesService.UpdateNote(noteUpdateRequest);

            // Assert
            action.Should().Throw<ArgumentNullException>();

        }

        [Fact]
        public void UpdateNote_NullTitle()
        {
            // Arrrange
            NoteUpdateRequest? noteUpdateRequest = 
                _fixture.Build<NoteUpdateRequest>()
                .With(tem => tem.Title, null as string)
                .Create();
            //Act 
            Action action = () => _notesService.UpdateNote(noteUpdateRequest);

            // Assert
            action.Should().Throw<ArgumentException>();

        }

        [Fact]
        public void UpdateNote_NullDueTime()
        {
            // Arrrange
            NoteUpdateRequest? noteUpdateRequest =
                _fixture.Build<NoteUpdateRequest>()
                .With(tem => tem.DueTime, (DateTime?)null)
                .Create();
            //Act 
            Action action = () => _notesService.UpdateNote(noteUpdateRequest);

            // Assert
            action.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void UpdateNote_NullID()
        {
            // Arrrange
            NoteUpdateRequest? noteUpdateRequest =
                _fixture.Build<NoteUpdateRequest>()
                .With(tem => tem.Id, (Guid?) null)
                .Create();
            //Act 
            Action action = () => _notesService.UpdateNote(noteUpdateRequest);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void UpdateNote_IdIsntRight()
        {
            // Arrrange
            NoteUpdateRequest? noteUpdateRequest =
                _fixture.Build<NoteUpdateRequest>()
                .With(tem => tem.Id, Guid.NewGuid())
                .Create();
            //Act 
            Action action = () => _notesService.UpdateNote(noteUpdateRequest);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void UpdateNote_ProperDetails()
        {
            // Arrrange
            NoteAddRequest? noteAddRequest =
                _fixture.Create<NoteAddRequest>();
            NoteResponse noteResponse_from_add = _notesService.AddNote(noteAddRequest);
            NoteUpdateRequest noteUpdateRequest = new NoteUpdateRequest()
            {
                Id = noteResponse_from_add.Id,
                Title = "Updated Title",
                CreatedTime = DateTime.Now,
                Description = noteResponse_from_add.Description,
                DueTime = noteResponse_from_add.DueTime,
                Status = noteResponse_from_add.Status
            };
            Note note= noteUpdateRequest.ToNote();
            NoteResponse noteResponse = note.ToNoteResponse();

            //Act 
            NoteResponse noteResponse_from_Update = _notesService.UpdateNote(noteUpdateRequest);

            // Assert
            noteResponse_from_Update.Should().BeEquivalentTo(noteResponse);
        }
        #endregion

        #region DeleteNote

        [Fact]
        public void DeleteNote_NullId()
        {
            // Arrange
            Guid? id = null;

            //Act
            Action action = () => _notesService.DeleteNote(id);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void DeleteNote_IdIsntRight_false()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            //Act
            bool isDeleted = _notesService.DeleteNote(id);

            // Assert
            isDeleted.Should().BeFalse();
        }

        [Fact]
        public void DeleteNote_ProperDetails_true()
        {
            // Arrange
            NoteAddRequest noteAddRequest = _fixture.Create<NoteAddRequest>();
            NoteResponse noteResponse = _notesService.AddNote(noteAddRequest);

            //Act
            bool isDeleted = _notesService.DeleteNote(noteResponse.Id);

            // Assert
            isDeleted.Should().BeTrue();
        }


        #endregion

    }
}