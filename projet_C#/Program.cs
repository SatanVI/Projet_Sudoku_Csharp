using System;

namespace SudokuConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // création d’un objet jeu Sudoku
            SudokuGame game = new SudokuGame();

            // lancement du jeu
            game.Run();
        }
    }
}