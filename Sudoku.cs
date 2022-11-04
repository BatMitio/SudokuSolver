namespace SudokuSolver;

public class Sudoku
{
    public int[][] Table { get; set; }
    private int[][][] Possibilities { get; set; }
    private int[][][] RowPossibilities { get; set; }
    private int[][][] ColPossibilities { get; set; }
    private int Dimension, SquareDimension;

    /// <summary>
    ///
    /// The dimension of the sudoku table
    /// 
    /// </summary>
    /// <param name="n">Should be divisible by 3</param>
    public Sudoku(int n)
    {
        Dimension = n;
        SquareDimension = n / 3;
        Table = new int[n][];
        for (int row = 0; row < n; ++row)
        {
            Table[row] = Enumerable.Repeat(0, n).ToArray();
        }

        Possibilities = new int[n][][];
        for (int row = 0; row < n; ++row)
        {
            Possibilities[row] = new int[n][];
        }
    }

    public void FillByRows(int rowNumber, int[] rowArray)
    {
        Table[rowNumber] = rowArray;
    }

    public void FillByIndex(int rowNumber, int colNumber, int val)
    {
        Table[rowNumber][colNumber] = val;
    }

    public void FillByMatrix(int[][] matrix)
    {
        Table = matrix;
    }

    private int[] FindMissing(int row, int col)
    {
        int squareRowStart = (row / SquareDimension) * SquareDimension;
        int squareColStart = (col / SquareDimension) * SquareDimension;
        HashSet<int> set = new HashSet<int>();
        int[] possibilities = new int[Dimension];
        for (int i = 0; i < Dimension; i++)
        {
            possibilities[i] = i + 1;
        }

        for (int r = squareRowStart; r < squareRowStart + SquareDimension; r++)
        {
            for (int c = squareColStart; c < squareColStart + SquareDimension; c++)
            {
                if (Table[r][c] != 0)
                    set.Add(Table[r][c]);
            }
        }

        for (int c = 0; c < Dimension; c++)
        {
            if (Table[row][c] != 0)
                set.Add(Table[row][c]);
        }

        for (int r = 0; r < Dimension; r++)
        {
            if (Table[r][col] != 0)
                set.Add(Table[r][col]);
        }

        return possibilities.Where(p => !set.Contains(p)).ToArray();
    }

    public bool FindAndFillOnlyPossibilitiesInRow(int row)
    {
        Dictionary<int, KeyValuePair<int, int>> occurence = new Dictionary<int, KeyValuePair<int, int>>();
        List<int> bannedNumbers = new List<int>();

        for (int col = 0; col < Dimension; col++)
        {
            if (Possibilities[row][col] is not null)
            {
                foreach (var number in Possibilities[row][col])
                {
                    if (!bannedNumbers.Contains(number))
                    {
                        if (occurence.ContainsKey(number))
                        {
                            bannedNumbers.Add(number);
                            occurence.Remove(number);
                        }
                        else
                        {
                            occurence.Add(number, new KeyValuePair<int, int>(row, col));
                        }
                    }
                }
            }
        }

        foreach (var number in occurence)
        {
            Table[number.Value.Key][number.Value.Value] = number.Key;
        }

        return occurence.Count > 0;
    }

    public bool FindAndFillOnlyPossibilitiesInCol(int col)
    {
        Dictionary<int, KeyValuePair<int, int>> occurence = new Dictionary<int, KeyValuePair<int, int>>();
        List<int> bannedNumbers = new List<int>();

        for (int row = 0; row < Dimension; row++)
        {
            if (Possibilities[row][col] is not null)
            {
                foreach (var number in Possibilities[row][col])
                {
                    if (!bannedNumbers.Contains(number))
                    {
                        if (occurence.ContainsKey(number))
                        {
                            bannedNumbers.Add(number);
                            occurence.Remove(number);
                        }
                        else
                        {
                            occurence.Add(number, new KeyValuePair<int, int>(row, col));
                        }
                    }
                }
            }
        }

        foreach (var number in occurence)
        {
            Table[number.Value.Key][number.Value.Value] = number.Key;
        }

        return occurence.Count > 0;
    }

    private bool FindSinglePossibilitiesAndFillTable()
    {
        bool found = false;
        for (int row = 0; row < Dimension; row++)
        {
            for (int col = 0; col < Dimension; col++)
            {
                if (Possibilities[row][col] is not null && Possibilities[row][col].Length == 1)
                {
                    Table[row][col] = Possibilities[row][col][0];
                    found = true;
                }
            }

            found = FindAndFillOnlyPossibilitiesInRow(row) || found;
        }

        for (int col = 0; col < Dimension; col++)
        {
            found = FindAndFillOnlyPossibilitiesInCol(col) || found;
        }

        return found;
    }

    private bool FillPossibilities()
    {
        for (int row = 0; row < Dimension; row++)
        {
            for (int col = 0; col < Dimension; col++)
            {
                if (Table[row][col] == 0)
                    Possibilities[row][col] = FindMissing(row, col);
            }
        }

        return FindSinglePossibilitiesAndFillTable();
    }

    public void Solve()
    {
        while (true)
        {
            while (FillPossibilities()) ;
            Console.WriteLine();
        }
    }

    public void Print()
    {
        for (int row = 0; row < Dimension; row++)
        {
            for (int col = 0; col < Dimension; col++)
            {
                Console.Write(Table[row][col]);
            }

            Console.WriteLine();
        }
    }
}