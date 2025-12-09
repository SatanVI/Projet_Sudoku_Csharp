using System;

namespace SudokuConsoleApp
{
    public class SudokuBoard
    {
        // tableau 9x9 pour les chiffres du Sudoku
        public int[,] Grid { get; private set; } = new int[9, 9];

        // tableau 9x9 pour indiquer si une case est fixe
        private bool[,] IsFixed { get; set; } = new bool[9, 9];

        // générateur de nombres aléatoires
        private Random random = new Random();

        // initialisation de la grille avec 1 ou 2 chiffres par bloc 3x3
        public void Initialize()
        {
            // vider la grille et les cases fixes
            Array.Clear(Grid, 0, Grid.Length);
            Array.Clear(IsFixed, 0, IsFixed.Length);

            // parcours de chaque bloc 3x3
            for (int boxRow = 0; boxRow < 3; boxRow++)
            {
                for (int boxCol = 0; boxCol < 3; boxCol++)
                {
                    int count = random.Next(1, 3); // choix de 1 ou 2 chiffres

                    // placement des chiffres
                    for (int k = 0; k < count; k++)
                    {
                        int num, r, c;
                        int tries = 0;

                        // tentative de placement d’un chiffre valide
                        do
                        {
                            num = random.Next(1, 10); // chiffre entre 1 et 9
                            r = boxRow * 3 + random.Next(3); // ligne dans le bloc
                            c = boxCol * 3 + random.Next(3); // colonne dans le bloc
                            tries++;
                        }
                        // répétition si case occupée ou coup invalide
                        while ((Grid[r, c] != 0 || !SudokuValidator.IsValidMove(Grid, r, c, num)) && tries < 20);

                        // placement réussi avant 20 essais
                        if (tries < 20)
                        {
                            Grid[r, c] = num;       // ajout du chiffre
                            IsFixed[r, c] = true;   // case marquée comme fixe
                        }
                    }
                }
            }
        }

        // affichage de la grille
        public void Display()
        {
            Console.WriteLine(" +-------+-------+-------+");
            for (int r = 0; r < 9; r++)
            {
                Console.Write(" | ");
                for (int c = 0; c < 9; c++)
                {
                    int val = Grid[r, c];
                    // affichage d’un point si vide, sinon chiffre
                    char ch = val == 0 ? '.' : (char)('0' + val);
                    Console.Write(ch + " ");
                    if (c == 2 || c == 5) Console.Write("| "); // séparateur de bloc
                }
                Console.WriteLine("|");
                if (r == 2 || r == 5 || r == 8)
                    Console.WriteLine(" +-------+-------+-------+");
            }
        }

        // tentative de placement d’un chiffre
        public string TryPlace(int row, int col, int value)
        {
            int r = row - 1; // ajustement car saisie utilisateur 1-9
            int c = col - 1;

            // vérification des limites
            if (r < 0 || r >= 9 || c < 0 || c >= 9) return "Erreur: hors limites.";

            // vérification si case fixe
            if (IsFixed[r, c]) return "Impossible: case de départ.";

            // vérification de la valeur
            if (value < 1 || value > 9) return "Erreur: chiffre entre 1 et 9.";

            // vérification des règles Sudoku
            if (!SudokuValidator.IsValidMove(Grid, r, c, value))
                return "Mouvement invalide (règles Sudoku non respectées).";

            // placement du chiffre
            Grid[r, c] = value;
            return "Coup accepté.";
        }

        // vérification si la grille est complète
        public bool IsComplete()
        {
            for (int r = 0; r < 9; r++)
                for (int c = 0; c < 9; c++)
                    if (Grid[r, c] == 0) return false; // case vide
            return true; // toutes les cases remplies
        }

        // réinitialisation des cases non fixes
        public void ResetNonFixed()
        {
            for (int r = 0; r < 9; r++)
                for (int c = 0; c < 9; c++)
                    if (!IsFixed[r, c]) Grid[r, c] = 0; // vider case libre
        }
    }
}