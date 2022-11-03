// See https://aka.ms/new-console-template for more information

using SudokuSolver;

var sudoku = new Sudoku(9);
sudoku.FillByMatrix(new[]
{
    new[] {0, 0, 0,  0, 7, 8,  0, 4, 1},
    new[] {1, 0, 0,  0, 0, 9,  0, 7, 0},
    new[] {3, 7, 4,  5, 6, 1,  8, 9, 2},
    
    new[] {0, 0, 1,  0, 8, 0,  0, 6, 7},
    new[] {0, 0, 0,  0, 0, 7,  0, 0, 0},
    new[] {0, 0, 7,  6, 0, 0,  0, 0, 4},
    
    new[] {4, 9, 6,  8, 0, 0,  7, 1, 5},
    new[] {2, 1, 8,  7, 4, 5,  0, 3, 0},
    new[] {7, 0, 0,  0, 9, 6,  4, 2, 8},
});

sudoku.Solve();