using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Player.Interface;

namespace BattleshipBot
{
    class ShipPositionMaker
    {
        public static List<IShipPosition> GenerateShipPositions()
        {

            var shipPositions = new List<IShipPosition>();

            var random = new Random();
            bool carrierPlaced = false;

            while (!carrierPlaced)
            {
                var carrierRow = random.Next(0, 9);
                var carrierCol = random.Next(0, 9);

                //picking direction
                var direction = random.Next(0, 1);
                var carrierPosition = new ShipPosition(null, null);
                if (direction == 0)
                {
                    carrierPosition = new ShipPosition(new GridSquare(ConvertIntToLetter(carrierRow), carrierCol), new GridSquare(ConvertIntToLetter(carrierRow), carrierCol + 5));
                }
                else if (direction == 1)
                {
                    carrierPosition = new ShipPosition(new GridSquare(ConvertIntToLetter(carrierRow), carrierCol), new GridSquare(ConvertIntToLetter(carrierRow + 5), carrierCol ));
                }
                if (IsValidPosition(carrierPosition, shipPositions))
                {
                    shipPositions.Add(carrierPosition);
                    carrierPlaced = true;
                }

            }
            return shipPositions;
        }


         public static bool IsValidPosition(ShipPosition ship, List<IShipPosition> list)
         {
             var startingSquare = ship.StartingSquare;
             var endingSquare = ship.EndingSquare;
             if (IsValidSquare(startingSquare) && IsValidSquare(endingSquare))
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
                    possibleGrids.Add(new GridSquare(ConvertIntToLetter(row),col));
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





    }
}
