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

        public static IGridSquare AttackTargetShip(IGridSquare lastSquare, IGridSquare lastSuccessfulSquare, List<IGridSquare> targetShip)
        {
            if (targetShip.Count == 1)
            {

                if (MyBot.lastdirection == null)
                {
                    MyBot.lastdirection = "above";
                    return Squares.GetSquareDirection(lastSquare, "above");
                }
                if (MyBot.lastdirection == "above")
                {
                    MyBot.lastdirection = "right";
                    return Squares.GetSquareDirection(lastSquare, "right");
                }
                if (MyBot.lastdirection == "right")
                {
                    MyBot.lastdirection = "below";
                    return Squares.GetSquareDirection(lastSquare, "below");
                }
                return Squares.GetSquareDirection(lastSquare, "left");
            }
            var direction = ShipPositionMaker.GetShipOrientation(targetShip);
            if (direction == "horizontal")
            {
                var leftSquare = ShipPositionMaker.GetLowestColumn(targetShip);
                var rightSquare = ShipPositionMaker.GetHighestColumn(targetShip);
                if (IsThisNewShot(Squares.GetSquareDirection(leftSquare,"left")))
                {
                    return (Squares.GetSquareDirection(leftSquare, "left"));
                }
                if (IsThisNewShot(Squares.GetSquareDirection(rightSquare, "right")))
                {
                    return (Squares.GetSquareDirection(rightSquare, "right"));
                }
                MyBot.attackMode = false;
                return RandomTarget();
            }
            if (direction == "vertical")
            {
                var topSquare = ShipPositionMaker.GetLowestRow(targetShip);
                var bottomSquare = ShipPositionMaker.GetHighestRow(targetShip);
                if (IsThisNewShot(Squares.GetSquareDirection(topSquare, "above")))
                {
                    return (Squares.GetSquareDirection(topSquare, "above"));
                }
                if (IsThisNewShot(Squares.GetSquareDirection(bottomSquare, "below")))
                {
                    return (Squares.GetSquareDirection(bottomSquare, "below"));
                }
                MyBot.attackMode = false;
                return RandomTarget();
            }
            return RandomTarget();
        }




        public static bool IsThisNewShot(IGridSquare newShot)
        {
            return !MyBot.allTargetsSoFar.Contains(newShot);
        }




        public static IGridSquare RandomTarget()
        {
            
            while (true)
            {
                var shipRow = MyBot.random.Next(1, 11);
                var shipCol = MyBot.random.Next(1, 11);
                var potentialSquare = new GridSquare(Squares.ConvertIntToLetter(shipRow), shipCol);
                if (IsThisNewShot(potentialSquare))
                {
                    return new GridSquare(Squares.ConvertIntToLetter(shipRow), shipCol);
                }
            }
        }



    }
}
