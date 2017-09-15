using System;
using Battleships.Player.Interface;
using System.Collections.Generic;

namespace BattleshipBot
{
    public class MyBot : IBattleshipsBot
    {
        private IGridSquare lastTarget;
        public static bool attackMode;
        public IGridSquare lastSuccessfulSquare;
        public List<IGridSquare> targetShip;
        public static Random random = new Random();
        public static string lastdirection;
        public static List<IGridSquare> allTargetsSoFar;

        public IEnumerable<IShipPosition> GetShipPositions()
        {
            lastTarget = null; // Forget all our history when we start a new game
            attackMode = false; // will go to true once scores hit until boat is destroyed.
            
            return ShipPositionMaker.GenerateShipPositions();
        }




        public IGridSquare SelectTarget()
        {
            return TargetMaker.RandomTarget();

            //var currentTarget = whatever
            //allTargetsSoFar.Add(currentTarget);
            //lastTarget = currentTarget
            //return currentTarget
            /*
                var nextTarget = TargetMaker.GetNextTarget(lastTarget);
                lastTarget = nextTarget;
                return nextTarget;
            */

        }


        public void HandleShotResult(IGridSquare square, bool wasHit)
        {
            if (wasHit && !attackMode)
            {

                attackMode = true;
                lastSuccessfulSquare = square;
                targetShip.Add(lastSuccessfulSquare);
                lastdirection = null;
            }
            else if (wasHit && attackMode)
            {
                lastSuccessfulSquare = square;
                targetShip.Add(lastSuccessfulSquare);
            }
            
            // Ignore whether we're successful
        }

        public void HandleOpponentsShot(IGridSquare square)
        {
            // Ignore what our opponent does
        }

        public string Name => "Chaos Bot";
    }
}
