using System;

namespace SudokuConsoleApp
{
    public class SudokuGame
    {
        // objet qui contient la grille du Sudoku
        private SudokuBoard _board = new SudokuBoard();

        public void Run()
        {
            // préparation de la grille
            _board.Initialize();

            // affichage des règles et commandes
            PrintInstructions();

            // boucle principale du jeu
            while (true)
            {
                // affichage de la grille
                _board.Display();
                Console.WriteLine();

                // demande de commande au joueur
                Console.WriteLine("Entrez votre commande (ex: 1 3 9) ou 'check', 'restart', 'quit':");
                Console.Write("> ");
                string input = Console.ReadLine()?.Trim().ToLower();

                // quitter le jeu
                if (input == "quit") break;

                // vérification si la grille est complète
                if (input == "check")
                {
                    Console.WriteLine(_board.IsComplete()
                        ? "Bravo, Sudoku complété !"   // message si terminé
                        : "Pas encore terminé ou erreurs présentes."); // message si non terminé
                    continue;
                }

                // redémarrage de la partie
                if (input == "restart")
                {
                    _board.ResetNonFixed(); // suppression des cases non fixes
                    Console.WriteLine("Grille réinitialisée.");
                    continue;
                }

                // séparation de la commande en trois parties : ligne, colonne, valeur
                string[] parts = input.Split(' ');
                if (parts.Length == 3 &&
                    int.TryParse(parts[0], out int row) &&   // ligne
                    int.TryParse(parts[1], out int col) &&   // colonne
                    int.TryParse(parts[2], out int val))     // valeur
                {
                    // tentative de placement de la valeur dans la grille
                    Console.WriteLine(_board.TryPlace(row, col, val));
                }
                else
                {
                    // commande incorrecte
                    Console.WriteLine("Commande invalide. Format: ligne colonne valeur.");
                }
            }

            // message de fin
            Console.WriteLine("Au revoir !");
        }

        private void PrintInstructions()
        {
            // effacement de l’écran
            Console.Clear();

            // affichage du titre
            Console.WriteLine("==========================================");
            Console.WriteLine("        Bienvenue dans Sudoku Console      ");
            Console.WriteLine("==========================================\n");

            // affichage des règles
            Console.WriteLine("                 RÈGLES");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("• Chaque ligne, colonne et sous-grille doit");
            Console.WriteLine("  contenir les chiffres de 1 à 9.");
            Console.WriteLine("• Les cases fixes ne peuvent pas être modifiées.\n");

            // affichage des commandes
            Console.WriteLine("               COMMANDES");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(" 'row col val' : jouer un coup (ex: 4 5 3)");
            Console.WriteLine(" 'check'       : vérifier si la grille est complète");
            Console.WriteLine(" 'restart'     : recommencer une nouvelle partie");
            Console.WriteLine(" 'quit'        : quitter le jeu\n");

            Console.WriteLine("==========================================");
        }
    }
}