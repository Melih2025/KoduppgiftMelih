using ColourMemoryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ColourMemoryGame.Services // Har koll på poängen och logiken för spelet.
{
    public class GameManager
    {
        public List<Card> Cards { get; private set; } // Lista med kort i spelbrädan, dock går bara ändras i gamemanager.cs
        public int Score { get; set; } //håller koll på spelarens poäng.

        public bool GameOver // kollar om spelet är slut, sedan retunerar true när alla kort är matchade, matchen är slut!
        {                  
            get
            {
                foreach (var card in Cards)
                {
                    if (!card.IsMatched) 
                        return false;        
                }
                return true; 
            }
        }

        private static readonly string[] AvailableColors =
            { "Red", "Green", "Blue", "Yellow", "Black", "Orange", "Pink", "White" };

        public GameManager()   // sätter start värde för korten.
        {
            Cards = new List<Card>();
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            Score = 0;
            Cards.Clear();

            // Skapar färg par, totalt 16 kort
            List<string> colorPairs = new List<string>();
            foreach (var color in AvailableColors)
            {
                colorPairs.Add(color);
                colorPairs.Add(color);
            }

            // byter plats på färgerna så att de hamnar slumpmässigt.
            Random rng = new Random();
            for (int i = colorPairs.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                string temp = colorPairs[i];
                colorPairs[i] = colorPairs[j];  // använder Fisher-Yates-algoritm för att blanda korten.
                colorPairs[j] = temp;

            }

            // Skapar korten från färgerna
            foreach (var color in colorPairs)
            {
                Cards.Add(new Card(color));
            }
        }

        public bool CheckMatch(Card first, Card second) // jämför färgen på två kort
        {
            return first.Color == second.Color;
        }

        public void MarkAsMatched(Card first, Card second) // Markera kort som är matchade
        {
            first.IsMatched = true;
            second.IsMatched = true;
        }
    }
}
