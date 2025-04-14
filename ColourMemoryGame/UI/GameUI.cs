using ColourMemoryGame.Models;
using ColourMemoryGame.Services;
using System;
using System.Threading;

namespace ColourMemoryGame.UI // Kör spelet
{
    public class GameUI
    {
        private readonly GameManager _gameManager; // för att kunna kommunicera med GameManager.cs klassen.

        public GameUI(GameManager gameManager) 
        {
            _gameManager = gameManager; // Detta gör att vi kan använda GameManager i hela klassen.
        }
        private void ShowInstructions() 
        {
            Console.Clear();
            Console.WriteLine("Välkommen till Colour Memory!");
            Console.WriteLine("Hur spelet går till:");
            Console.WriteLine("- Match leder till +1 poäng");
            Console.WriteLine("- Ingen Match leder till -1 poäng");
            Console.WriteLine();
            Console.WriteLine("Tryck på valfri tangent för att starta spelet...");
            Console.ReadKey();
        }


        public void PlayGame()
        {

            ShowInstructions();


            while (!_gameManager.GameOver) // Så länge inte spelet är över (false), fortsätt loopen, 
                                           // Vi använder oss av GameOver i GameManager.cs
            {

                Console.Clear();

                DisplayBoard();

                Console.WriteLine($"Poäng: {_gameManager.Score}\n");

              var firstCard = AskForCard("Välj det första kortet (1-16): "); // stopar spelet, väntar på input.
                firstCard.IsFaceUp = true;

                Console.Clear();
                DisplayBoard();

                var secondCard = AskForCard("Välj det andra kortet (1-16): ");
                secondCard.IsFaceUp = true;

                Console.Clear();
                DisplayBoard();

                if (_gameManager.CheckMatch(firstCard, secondCard)) 
                {
                    Console.WriteLine("\nMatch! +1 Poäng.");
                    _gameManager.MarkAsMatched(firstCard, secondCard);
                    _gameManager.Score++; //adderar score med poäng
                    Thread.Sleep(2000); // väntar 2 sekunder ifall det blir rätt match.

                }
                else
                {
                    Console.WriteLine("\nIngen match! -1 Poäng.");
                    if (_gameManager.Score > 0)
                        _gameManager.Score--; // minskar score -1 poäng

                    Thread.Sleep(2000); // väntar 2 sekunder ifall det blir fel match.
                    firstCard.IsFaceUp = false;
                    secondCard.IsFaceUp = false; // går tillbaka till standard värde.
                }


                Console.WriteLine("\nTryck valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
            Console.Clear();
            Console.WriteLine($"\nSpelet är över! Slutpoäng: {_gameManager.Score}"); 
        }

        private void DisplayBoard() // skapar boarden 4x4
        {
            for (int i = 0; i < _gameManager.Cards.Count; i++)
            {
                var card = _gameManager.Cards[i];
                string display = GetCardDisplay(card, i).PadLeft(2); 
                Console.Write($"[{display}] "); //  Returnerar vad som ska visas för ett kort beroende på tillstånd.

                if ((i + 1) % 4 == 0)
                Console.WriteLine("\n");
            }
        }

        private string GetCardDisplay(Card card, int index) // Metod för vad som ska visas när kortet är uppvänt.
        {
            if (card.IsMatched) return "✓ ";
            if (card.IsFaceUp) return card.Color.PadRight(2);  
            return (index + 1).ToString().PadRight(2);
        }

        private Card AskForCard(string prompt) // hanterar användarens input, samt fel hantering.
                                           // loopen körs inuti GameOver metoden för att vänta på input.
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int cardNumber))
                {
                    Console.WriteLine("Skriv en siffra mellan 1-16.");
                    continue;
                }

                if (cardNumber < 1 || cardNumber > 16)
                {
                    Console.WriteLine("Välj ett kort mellan 1 och 16.");
                    continue;
                }

                Card selectedCard = _gameManager.Cards[cardNumber - 1];

                if (selectedCard.IsFaceUp)
                {
                    Console.WriteLine("Det här kortet är redan uppvänt.");
                    continue;
                }

                if (selectedCard.IsMatched)
                {
                    Console.WriteLine("Det här kortet är redan matchat.");
                    continue;
                }

                return selectedCard;
            }
        }
    }
}