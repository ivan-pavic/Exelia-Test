using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CS_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] goodSudoku1 = {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},

                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},

                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };




            int[][] badSudoku1 =  {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
            };

            int[][] badSudoku2 = {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},
                new int[] {1,2,3,4},
                new int[] {1}
            };

            int[][] goodSudoku2 = {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };

            Debug.Assert(ValidateSudoku(goodSudoku1), "This is supposed to validate! It's a good sudoku!");
            Debug.Assert(ValidateSudoku(goodSudoku2), "This is supposed to validate! It's a good sudoku!");
            Debug.Assert(!ValidateSudoku(badSudoku1), "This isn't supposed to validate! It's a bad sudoku!");
            Debug.Assert(!ValidateSudoku(badSudoku2), "This isn't supposed to validate! It's a bad sudoku!");
        }

        static bool ValidateSudoku(int[][] puzzle)
        {
            return (new Sudoku(puzzle)).IsValid();
        }
    }

    public class Sudoku
    {
        private int[][] sudoku;

        public Sudoku(int[][] sudoku)
        {
            this.sudoku = sudoku;
        }

        public bool IsValid()
        {
            var square = sudoku.GetSquares();

            return IsValidFormat() && ArePartsValid(sudoku.GetRows()) && ArePartsValid(sudoku.GetColumns()) && ArePartsValid(sudoku.GetSquares());
        }

        private bool IsValidFormat()
        {
            return this.sudoku.Length > 0
                && Math.Sqrt(sudoku.Length) % 1 == 0
                && !sudoku.Any(line => line.Length != sudoku.Length || line.Any(element => element > sudoku.Length || element <= 0));
        }
        private bool ArePartsValid(IEnumerable<IEnumerable<int>> part)
        {
            return !part.Any(element => !element.IsValidSudokuPart());
        }
    }


    static class ExtensionMethods
    {

        public static IEnumerable<IEnumerable<int>> GetRows(this int[][] jaggedArray)
        {
            return jaggedArray;
        }

        public static IEnumerable<IEnumerable<int>> GetColumns(this int[][] jaggedArray)
        {
            return Enumerable.Range(0, jaggedArray.Length).Select(x => jaggedArray.Select(row => row[x]));
        }

        public static IEnumerable<IEnumerable<int>> GetSquares(this int[][] jaggedArray)
        {
            int arraySquareSize = (int)Math.Sqrt(jaggedArray.Length);

            return from xStartingPoint in Enumerable.Range(0, arraySquareSize)
                   from yStartingPoint in Enumerable.Range(0, arraySquareSize)
                   select (
                       from x in Enumerable.Range(0, arraySquareSize)
                       from y in Enumerable.Range(0, arraySquareSize)
                       select jaggedArray[xStartingPoint * arraySquareSize + x][yStartingPoint * arraySquareSize + y]);
        }

        public static bool IsValidSudokuPart(this IEnumerable<int> sudokuPart)
        {
            return !sudokuPart.GroupBy(number => number).Any(number => number.Count() > 1);
        }
    }
}