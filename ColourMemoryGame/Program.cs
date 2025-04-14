using ColourMemoryGame.Services;
using ColourMemoryGame.UI;

class Program
{
    static void Main()
    {
        while (true)
        {
            var gameManager = new GameManager();
            var ui = new GameUI(gameManager);

            ui.PlayGame(); // anropar GameUI metoden PlayGame för att starta spelet.

            while (true)
            {
                Console.Write("\nVill du spela igen? (j/n): ");
                var input = Console.ReadLine()?.ToLower();

                if (input == "j")
                    break; // fortsätter att loopen och startar ett nytt spel

                if (input == "n")
                    return; // avslutar hela programmet

                Console.WriteLine("Ogiltigt svar. Vänligen skriv 'j' för ja eller 'n' för nej.");
            }
        }
    }
}
