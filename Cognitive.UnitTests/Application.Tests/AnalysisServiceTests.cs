
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Core.Interfaces;
using Application.Services;

namespace Application.Tests
{
    public class AnalysisServiceTests
    {
        [Fact]
        public async Task ScoreTextAsync_ReturnsCorrectScore()
        {
            // Arrange
            var mockRepo = new Mock<IWordRepository>();
            var inputText = "happy sad neutral";
            var words = new List<string> { "happy", "sad", "neutral" };

            mockRepo.Setup(repo => repo.GetWordScoresAsync(words))
                    .ReturnsAsync(3); // Simulate a score

            var service = new JournalScoringService(mockRepo.Object);

            // Act
            var result = await service.ScoreTextAsync(inputText);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task ScoreTextAsync_HandlesEmptyInput()
        {
            // Arrange
            var mockRepo = new Mock<IWordRepository>();
            var service = new JournalScoringService(mockRepo.Object);

            // Act
            var result = await service.ScoreTextAsync("");

            // Assert
            mockRepo.Verify(repo => repo.GetWordScoresAsync(It.IsAny<List<string>>()), Times.Once);
        }
    }
}
