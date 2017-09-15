using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Battleships.Player.Interface;


namespace BattleshipBot
{
    class TargetMaker
    {

        public static IGridSquare GetNextTarget(IGridSquare lastTarget)
        {
            if (lastTarget == null)
            {
                return new GridSquare('A', 1);
            }
            var possibleTarget = new GridSquare(Squares.IncrementRowBy(lastTarget.Row, 1), lastTarget.Column + 1);
            if (Squares.IsValidSquare(possibleTarget))
            {
                return possibleTarget;
            }

            var diagonalList = new List<GridSquare>()
            {
                new GridSquare('A', 3),
                new GridSquare('A', 5),
                new GridSquare('A', 7),
                new GridSquare('A', 9),
                new GridSquare('C', 1),
                new GridSquare('E', 1),
                new GridSquare('G', 1),
                new GridSquare('I', 1)
            };
            var diagonalEnumerator = diagonalList.GetEnumerator();
            diagonalEnumerator.MoveNext();
            var current = diagonalEnumerator.Current;
            diagonalEnumerator.Dispose();
            return current;
        }

        public static IGridSquare AttackTargetShip(IGridSquare lastSquare, IGridSquare lastSuccessfulSquare, List<IGridSquare> targetShip, List<IGridSquare> listTargetsSoFar)
        {
            if (targetShip.Count == 1)
            {

                if (MyBot.lastdirection == null && IsThisNewShot(Squares.GetSquareDirection(lastSquare, "above"),listTargetsSoFar))
                {
                    MyBot.lastdirection = "above";
                    if (IsThisNewShot(Squares.GetSquareDirection(lastSquare, "above"), listTargetsSoFar))
                    {
                        return Squares.GetSquareDirection(lastSquare, "above");
                    }
                }
                if (MyBot.lastdirection == "above")
                {
                    MyBot.lastdirection = "right";
                    if (IsThisNewShot(Squares.GetSquareDirection(lastSquare, "right"), listTargetsSoFar))
                    {
                        return Squares.GetSquareDirection(lastSquare, "right");
                    }
                }
                if (MyBot.lastdirection == "right")
                {
                    MyBot.lastdirection = "below";
                    if (IsThisNewShot(Squares.GetSquareDirection(lastSquare, "below"), listTargetsSoFar))
                    {
                        return Squares.GetSquareDirection(lastSquare, "below");
                    }
                }
                if (IsThisNewShot(Squares.GetSquareDirection(lastSquare, "left"), listTargetsSoFar))
                {
                    return Squares.GetSquareDirection(lastSquare, "left");
                }
            }
            var direction = ShipPositionMaker.GetShipOrientation(targetShip);
            if (direction == "horizontal")
            {
                var leftSquare = ShipPositionMaker.GetLowestColumn(targetShip);
                var rightSquare = ShipPositionMaker.GetHighestColumn(targetShip);
                if (IsThisNewShot(Squares.GetSquareDirection(leftSquare,"left"), listTargetsSoFar))
                {
                    return (Squares.GetSquareDirection(leftSquare, "left"));
                }
                if (IsThisNewShot(Squares.GetSquareDirection(rightSquare, "right"), listTargetsSoFar))
                {
                    return (Squares.GetSquareDirection(rightSquare, "right"));
                }
                MyBot.attackMode = false;
                return RandomTarget(listTargetsSoFar);
            }
            if (direction == "vertical")
            {
                var topSquare = ShipPositionMaker.GetHighestRow(targetShip);
                var bottomSquare = ShipPositionMaker.GetLowestRow(targetShip);
                if (IsThisNewShot(Squares.GetSquareDirection(topSquare, "above"), listTargetsSoFar))
                {
                    return (Squares.GetSquareDirection(topSquare, "above"));
                }
                if (IsThisNewShot(Squares.GetSquareDirection(bottomSquare, "below"), listTargetsSoFar))
                {
                    return (Squares.GetSquareDirection(bottomSquare, "below"));
                }
                MyBot.attackMode = false;
                return RandomTarget(listTargetsSoFar);
            }
            return RandomTarget(listTargetsSoFar);
        }




        public static bool IsThisNewShot(IGridSquare newShot, List<IGridSquare> allTargetsSoFar)
        {
            if (!allTargetsSoFar.Contains(newShot) && Squares.IsValidSquare(newShot))
            {
                return true;
            }
            return false;
        }




        public static IGridSquare RandomTarget(List<IGridSquare> listTargetsSoFar)
        {
            
            while (true)
            {
                var shipRow = MyBot.random.Next(1, 11);
                var shipCol = MyBot.random.Next(1, 11);
                var potentialSquare = new GridSquare(Squares.ConvertIntToLetter(shipRow), shipCol);
                if (IsThisNewShot(potentialSquare, listTargetsSoFar))
                {
                    return new GridSquare(Squares.ConvertIntToLetter(shipRow), shipCol);
                }
            }
        }



    }
}
