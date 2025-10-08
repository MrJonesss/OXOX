using Domain.Exceptions;
using Domain.Models;
using Xunit;

namespace Domain.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            var player = new Player("Alice", 'X', true);

            Assert.Equal("Alice", player.Name);
            Assert.Equal('X', player.Symbol);
            Assert.True(player.IsAi);
            Assert.Equal(0, player.Score);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_InvalidName_ShouldThrowException(string invalidName)
        {
            Assert.Throws<InvalidPlayerNameException>(() => new Player(invalidName, 'X'));
        }

        [Theory]
        [InlineData('A')]
        [InlineData(' ')]
        [InlineData('1')]
        public void Constructor_InvalidSymbol_ShouldThrowException(char invalidSymbol)
        {
            Assert.Throws<InvalidPlayerSymbolException>(() => new Player("Bob", invalidSymbol));
        }

        [Fact]
        public void AddWin_ShouldIncrementScore()
        {
            var player = new Player("Alice", 'X');
            Assert.Equal(0, player.Score);

            player.AddWin();
            Assert.Equal(1, player.Score);

            player.AddWin();
            Assert.Equal(2, player.Score);
        }

        [Fact]
        public void ToString_ShouldReturnExpectedFormat_Human()
        {
            var player = new Player("Alice", 'X', false);
            var str = player.ToString();

            Assert.Contains("Alice", str);
            Assert.Contains("X", str);
            Assert.Contains("Human", str);
            Assert.Contains("Score: 0", str);
        }

        [Fact]
        public void ToString_ShouldReturnExpectedFormat_AI()
        {
            var player = new Player("Bot", 'O', true);
            var str = player.ToString();

            Assert.Contains("Bot", str);
            Assert.Contains("O", str);
            Assert.Contains("AI", str);
            Assert.Contains("Score: 0", str);
        }
    }
}
