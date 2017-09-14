using System;
using System.Collections.Generic;
using System.Linq;
using Battleships.Player.Interface;

namespace BattleshipBot
{
    class ShipPositionMaker
    {
        public static List<IShipPosition> GenerateShipPositions()
        {

            var shipPositions = new List<IShipPosition>();

            var random = new Random();
            var shipPlaced = false;
            var shipsToPlace = new List<int>() {5, 4, 3, 3, 2};

            foreach (int length in shipsToPlace)
            {
                while (!shipPlaced)
                {
                    var shipRow = random.Next(0, 9);
                    var shipCol = random.Next(0, 9);


                    var direction = random.Next(0, 1);
                    var carrierPosition = new ShipPosition(null, null);
                    if (direction == 0)
                    {
                        carrierPosition = new ShipPosition(new GridSquare(ConvertIntToLetter(shipRow), shipCol),
                            new GridSquare(ConvertIntToLetter(shipRow), shipCol + (length - 1)));
                    }
                    else if (direction == 1)
                    {
                        carrierPosition = new ShipPosition(new GridSquare(ConvertIntToLetter(shipRow), shipCol),
                            new GridSquare(ConvertIntToLetter(shipRow + (length - 1)), shipCol));
                    }
                    if (IsValidPosition(carrierPosition, shipPositions))
                    {
                        shipPositions.Add(carrierPosition);
                        shipPlaced = true;
                    }

                }
            }
            return shipPositions;
        }


        public static bool IsValidPosition(ShipPosition ship, List<IShipPosition> list)
        {
            var startingSquare = ship.StartingSquare;
            var endingSquare = ship.EndingSquare;
            if (IsValidSquare(startingSquare) && IsValidSquare(endingSquare) && IsNotAdjacentToCurrentShip(ship, list))
            {
                return true;
            }
            {
                return false;
            }
        }


        public static bool IsValidSquare(IGridSquare square)
        {
            var possibleGrids = new List<IGridSquare>();
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
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
        

        public static bool IsNotAdjacentToCurrentShip(ShipPosition ship, List<IShipPosition> list)
        {
            var occupiedSquares = new List<GridSquare>();
            foreach (var boat in list)
            {
                occupiedSquares.AddRange(AllShipSquares(boat));
            }
            var testBoatSquares = AllShipSquares(ship);
            return !(from square in testBoatSquares from occupiedSquare in occupiedSquares where AreSquaresAdjacent(occupiedSquare, square) select square).Any();
        }
        

        public static List<GridSquare> AllShipSquares(IShipPosition ship)
        {
            var startsquare = ship.StartingSquare;
            var endsquare = ship.EndingSquare;
            var shipSquares = new List<GridSquare>();
            if (startsquare.Row == endsquare.Row)
            {
                if (startsquare.Column < endsquare.Column)
                {
                    for (var i = startsquare.Column; i <= endsquare.Column; i++)
                    {
                        shipSquares.Add(new GridSquare(startsquare.Row, i));
                    }
                }
                {
                    for (var i = endsquare.Column; i <= startsquare.Column; i++)
                    {
                        shipSquares.Add(new GridSquare(startsquare.Row, i));
                    }
                }

            }
            {
                if (startsquare.Row < endsquare.Row)
                {
                    for (var i = startsquare.Row; i <= endsquare.Row; i++)
                    {
                        shipSquares.Add(new GridSquare(i, startsquare.Column));
                    }
                }
                {
                    for (var i = endsquare.Row; i <= startsquare.Row; i++)
                    {
                        shipSquares.Add(new GridSquare(i, startsquare.Column));
                    }
                }
            }
            return shipSquares;

        }

        public static bool AreSquaresAdjacent(GridSquare square1, GridSquare square2)
        {

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    var testsquare = new GridSquare(ConvertIntToLetter(ConvertLetterToINt(square1.Row) + i),
                        square1.Column + j);
                    if (testsquare.Equals(square2))
                    {
                        return true;
                    }
                }
            }
            return false;
        }









        public static char ConvertIntToLetter(int num)
        {
            var letterDictionary = new Dictionary<int, char>()
            {
                {-1, ' ' },
                {0, 'A'},
                {1, 'B'},
                {2, 'C'},
                {3, 'D'},
                {4, 'E'},
                {5, 'F'},
                {6, 'G'},
                {7, 'H'},
                {8, 'I'},
                {9, 'J'},
                {10, 'K'},
                {11,'L' },
                {12, 'M' },
                {13, 'N' },
                {14, 'O' }
            };

            return letterDictionary[num];
        }
        public static int ConvertLetterToINt(char c)
        {
            var letterDictionary = new Dictionary<char, int>()
            {

                {'A',0},
                {'B', 1},
                {'C', 2},
                {'D', 3},
                {'E', 4},
                {'F', 5},
                {'G', 6},
                {'H', 7},
                {'I', 8},
                {'J', 9},
                {'K', 10},
                {'L', 11 },
                { 'M' , 12},
                { 'N', 13 },
                {'O', 14 }
            };

            return letterDictionary[c];
        }




    }
}
