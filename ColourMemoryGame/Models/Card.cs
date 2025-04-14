
namespace ColourMemoryGame.Models
{
    public class Card  // properties för Card
    {
        public string Color { get; set; }
        public bool IsFaceUp { get; set; }
        public bool IsMatched { get; set; }

        public Card(string color) 
        {
            Color = color;
            IsFaceUp = false;
            IsMatched = false;
        }
    }
}