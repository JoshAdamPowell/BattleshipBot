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
        public static Random random;
        public static string lastdirection;
        private static List<IGridSquare> allTargetsSoFar = new List<IGridSquare>();
        public static IGridSquare lastAttackTarget;





        public IEnumerable<IShipPosition> GetShipPositions()
        {
            lastTarget = null; // Forget all our history when we start a new game
            attackMode = false; // will go to true once scores hit until boat is destroyed.
            //From below are new additions here.
            random = new Random();
            lastdirection = null;
            allTargetsSoFar.Clear();
            lastAttackTarget = null;
            lastSuccessfulSquare = null;
            targetShip = new List<IGridSquare>();
            return ShipPositionMaker.GenerateShipPositions();
        }




        public IGridSquare SelectTarget()
        {

            IGridSquare currentTarget;
            if (!attackMode)
            {
                targetShip.Clear();
                currentTarget = TargetMaker.GetNextTarget(lastTarget,  allTargetsSoFar);
                allTargetsSoFar.Add(currentTarget);
                lastTarget = currentTarget;
            }
            else
            {
                currentTarget = TargetMaker.AttackTargetShip(lastAttackTarget, lastSuccessfulSquare, targetShip, allTargetsSoFar);
                if (currentTarget == null)
                {
                    targetShip.Clear();
                    currentTarget = TargetMaker.GetNextTarget(lastTarget, allTargetsSoFar);
                    allTargetsSoFar.Add(currentTarget);
                    lastTarget = currentTarget;
                    return currentTarget;
                }
                allTargetsSoFar.Add(currentTarget);
                lastAttackTarget = currentTarget;
            }
            return currentTarget;


        }


        public void HandleShotResult(IGridSquare square, bool wasHit)
        {
            if (wasHit && !attackMode)
            {

                attackMode = true;
                lastSuccessfulSquare = square;
                targetShip.Add(lastSuccessfulSquare);
                lastdirection = null;
                lastAttackTarget = square;
            }
            else if (wasHit && attackMode)
            {
                lastSuccessfulSquare = square;
                targetShip.Add(lastSuccessfulSquare);
                lastAttackTarget = square;
            }
            
            // Ignore whether we're successful
        }

        public void HandleOpponentsShot(IGridSquare square)
        {
            // Ignore what our opponent does
        }

        public string Name => "I say Botato and you say Bot-ah-to";
    }
}
