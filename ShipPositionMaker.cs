using System;
using System.Collections.Generic;
using System.Linq;
using Battleships.Player.Interface;

namespace BattleshipBot
{
    class ShipPositionMaker
    {
        public static IEnumerable<IShipPosition> GenerateShipPositions()
        {

            var shipPositions = new List<IShipPosition>();

            var random = new Random();
            
            var shipsToPlace = new List<int>() {5, 4, 3, 3, 2};

            foreach (int length in shipsToPlace)
            {
                var shipPlaced = false;
                while (!shipPlaced)
                {
                    var shipRow = random.Next(1, 11);
                    var shipCol = random.Next(1, 11);


                    var direction = random.Next(0, 2);
                    var shipPosition = new ShipPosition(null, null);
                    if (direction == 0)
                    {
                        shipPosition = new ShipPosition(new GridSquare(Squares.ConvertIntToLetter(shipRow), shipCol),
                            new GridSquare(Squares.ConvertIntToLetter(shipRow), shipCol + (length - 1)));
                    }
                    //fix vertical positions
                    else if (direction == 1)
                    {
                        shipPosition = new ShipPosition(new GridSquare(Squares.ConvertIntToLetter(shipRow), shipCol),
                            new GridSquare(Squares.ConvertIntToLetter(shipRow + (length - 1)), shipCol));
                    }
                    if (IsValidPosition(shipPosition, shipPositions))
                    {
                        shipPositions.Add(shipPosition);
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
            if (Squares.IsValidSquare(startingSquare) && Squares.IsValidSquare(endingSquare) && IsNotAdjacentToCurrentShip(ship, list))
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
            return !(from square in testBoatSquares from occupiedSquare in occupiedSquares where Squares.AreSquaresAdjacent(occupiedSquare, square) select square).Any();
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

        public static string GetShipOrientation(List<IGridSquare> ship)
        {
            if (ship[0].Row == ship[1].Row)
            {
                return "horizontal";
            }
            return "vertical";
        }


        public static IGridSquare GetLowestColumn(List<IGridSquare> ship)
        {
            var etst = ship.Min(x => x.Column);
            return new GridSquare(ship[0].Row, etst);
        }

        public static IGridSquare GetHighestColumn(List<IGridSquare> ship)
        {
            var etst = ship.Max(x => x.Column);
            return new GridSquare(ship[0].Row, etst);
        }

        public static IGridSquare GetLowestRow(List<IGridSquare> ship)
        {

            var etst = ship.Min(x => Squares.ConvertLetterToINt(x.Row));

            return new GridSquare(Squares.ConvertIntToLetter(etst), ship[0].Column);
        }

        public static IGridSquare GetHighestRow(List<IGridSquare> ship)
        {
            List<int> highestList = new List<int>();
            foreach (var grid in ship)
            {
                highestList.Add(Squares.ConvertLetterToINt(grid.Row));
            }
            int etst = highestList.Max();

            return new GridSquare(Squares.ConvertIntToLetter(etst), ship[0].Column);
        }





    }
}
