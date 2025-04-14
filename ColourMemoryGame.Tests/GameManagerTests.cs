

using ColourMemoryGame.Services;
using FluentAssertions;
using Xunit;

namespace ColourMemoryGame.Tests
{
    public class GameManagerTests
    {
        private readonly GameManager _game = new GameManager(); 

        [Fact]
        public void Should_have_16_cards_when_game_starts()
        {
            _game.Cards.Should().HaveCount(16, "because the game starts with 16 cards");
        }

        [Fact]
        public void Should_find_matching_cards()
        {
            var card1 = _game.Cards[0];
            var card2 = _game.Cards[1];

            _game.CheckMatch(card1, card2).Should().Be(card1.Color == card2.Color);
        }

        [Fact]
        public void Should_end_game_when_all_cards_matched()
        {
            foreach (var card in _game.Cards)
                card.IsMatched = true;

            _game.GameOver.Should().BeTrue("because all cards are matched");
        }
    }
}