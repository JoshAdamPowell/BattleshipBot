using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Battleships.Player.Interface;

namespace BattleshipBot
{
    public class Squares
    {
        public static bool AreSquaresAdjacent(GridSquare square1, GridSquare square2)
        {

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    var testsquare = new GridSquare(ConvertIntToLetter(ConvertLetterToINt(square1.Row) + i),
                        square1.Column + j);
                    if (testsquare.Row == square2.Row && testsquare.Column == square2.Column)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static char IncrementRowBy(char row, int increment)
        {
            return ConvertIntToLetter(ConvertLetterToINt(row) + increment);
        }

        public static IGridSquare GetSquareDirection(IGridSquare square, string direction)
        {
            switch (direction)
            {
                case "above":
                    return new GridSquare(IncrementRowBy(square.Row, 1), square.Column);
                case "right":
                    return new GridSquare(square.Row, square.Column + 1);
                case "below":
                    return new GridSquare(IncrementRowBy(square.Row, -1), square.Column);
                case "left":
                    return new GridSquare(square.Row, square.Column - 1);
                default:
                    return null;
            }
                

        }


        public static bool IsValidSquare(IGridSquare square)
        {
            var possibleGrids = new List<IGridSquare>();
            for (int row = 1; row < 11; row++)
            {
                for (int col = 1; col < 11; col++)
                {
                    possibleGrids.Add(new GridSquare(ConvertIntToLetter(row), col));
                }
            }
            if (possibleGrids.Contains(square))
            {
                return true;
            }
            {
                return false;
            }
        }



        public static char ConvertIntToLetter(int num)
        {
            var letterDictionary = new Dictionary<int, char>()
            {
                {-1, ' ' },
                {0, ' '},
                {1, 'A'},
                {2, 'B'},
                {3, 'C'},
                {4, 'D'},
                {5, 'E'},
                {6, 'F'},
                {7, 'G'},
                {8, 'H'},
                {9, 'I'},
                {10, 'J'},
                {11,'K' },
                {12, 'L' },
                {13, 'M' },
                {14, 'N' }
            };

            return letterDictionary[num];
        }
        public static int ConvertLetterToINt(char c)
        {
            var letterDictionary = new Dictionary<char, int>()
            {

                {'A',1},
                {'B', 2},
                {'C', 3},
                {'D', 4},
                {'E', 5},
                {'F', 6},
                {'G', 7},
                {'H', 8},
                {'I', 9},
                {'J', 10},
                {'K', 11},
                {'L', 12 },
                { 'M' , 13},
                { 'N', 14 },
                {'O', 15 }
            };

            return letterDictionary[c];
        }

    }
}
