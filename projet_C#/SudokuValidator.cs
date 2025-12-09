namespace SudokuConsoleApp
{
    public static class SudokuValidator
    {
        public static bool IsValidMove(int[,] grid, int r, int c, int value)
        {
            // vérification de la ligne
            for (int x = 0; x < 9; x++)
                if (grid[r, x] == value) return false;

            // vérification de la colonne
            for (int y = 0; y < 9; y++)
                if (grid[y, c] == value) return false;

            // vérification de la sous-grille 3x3
            int boxRowStart = (r / 3) * 3;
            int boxColStart = (c / 3) * 3;
            for (int rr = boxRowStart; rr < boxRowStart + 3; rr++)
                for (int cc = boxColStart; cc < boxColStart + 3; cc++)
                    if (grid[rr, cc] == value) return false;

            // mouvement valide
            return true;
        }
    }
}