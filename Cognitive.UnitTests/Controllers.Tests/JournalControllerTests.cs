using APP.Application.Common;
using APP.Controllers;
using Application.DTOs;
using Application.Services;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;



namespace Cognitive.UnitTests.Tests.Controllers.Tests
{
    public class JournalControllerTests
    {
        private readonly Mock<IJournalRepository> _journalRepoMock;
        private readonly Mock<IJournalScoringService> _scoringServiceMock;
        private readonly JournalController _controller;

        public JournalControllerTests()
        {
            _journalRepoMock = new Mock<IJournalRepository>();
            _scoringServiceMock = new Mock<IJournalScoringService>();
            _controller = new JournalController(_journalRepoMock.Object, _scoringServiceMock.Object);
        }

        [Fact]
        public async Task Submit_ShouldScoreAndSaveJournalEntry()
        {
            // Arrange
            var dto = new JournalEntryDto { Text = "Today I feel great" };
            int expectedScore = 9;

            _scoringServiceMock
                .Setup(s => s.ScoreTextAsync(dto.Text))
                .ReturnsAsync(expectedScore);

            _journalRepoMock
                .Setup(r => r.AddAsync(It.IsAny<JournalEntry>()))
                .Callback<JournalEntry>(entry =>
                {
                    entry.Id = 1;
                    entry.Score = expectedScore;
                })
                 .ReturnsAsync((JournalEntry entry) => entry);

            // Act
            var result = await _controller.Submit(dto);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var value = ok.Value;
            var id = (int?)value?.GetType().GetProperty("Id")?.GetValue(value);
            var score = (int?)value?.GetType().GetProperty("Score")?.GetValue(value);

            Assert.Equal(1, id);
            Assert.Equal(expectedScore, score);
        }

        [Fact]
        public async Task GetJournalById_ShouldReturnNotFound_WhenNoEntriesExist()
        {
            // Arrange
            int journalId = 42;

            _journalRepoMock
                  .Setup(r => r.GetJournalByIdAsync(journalId))
                  .Returns(Task.FromResult<JournalEntry?>(null));
            // Act
            var result = await _controller.GetJournalById(journalId);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"no journals found with id = {journalId}", notFound.Value);
        }

        [Fact]
        public async Task GetJournalById_ShouldReturnListOfEntries_WhenFound()
        {
            // Arrange
            int journalId = 1;
            var entry = new JournalEntry
            {
                 Id = 1, Text = "Entry one", Score = 7 
            };

            _journalRepoMock
                .Setup(r => r.GetJournalByIdAsync(journalId))
                .ReturnsAsync(entry);

            // Act
            var result = await _controller.GetJournalById(journalId);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var returnedEntries = Assert.IsType<JournalEntry>(ok.Value);
            Assert.Equal("Entry one", returnedEntries.Text);
            Assert.Equal(7, returnedEntries.Score);
        }


       
    }
}
