﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Player.Interface;
using NUnit.Framework;

namespace BattleshipBot
{
    [TestFixture]
    class ShipTester
    {
        [Test]
        public void CanGetAllShipLocations()
        {
            var testShip = new ShipPosition(new GridSquare('A', 1), new GridSquare('E', 1));
            var testList = ShipPositionMaker.AllShipSquares(testShip);
            var expectedList = new List<GridSquare>()
            {
                new GridSquare('A', 1),
                new GridSquare('B', 1),
                new GridSquare('C', 1),
                new GridSquare('D', 1),
                new GridSquare('E', 1),
            };

            Assert.AreEqual(expectedList, testList);
        }

        [Test]
        public void CanDetectAdjacentSquares()
        {
            var square1 = new GridSquare('C', 1);
            var square2 = new GridSquare('D', 1);
            var test = ShipPositionMaker.AreSquaresAdjacent(square1, square2);
            Assert.AreEqual(true,test);
        }

        [Test]
        public void WillNotDetectAdjacentSquaresWhenTheyArent()
        {
            var square1 = new GridSquare('A', 1);
            var square2 = new GridSquare('E', 2);
            var test = ShipPositionMaker.AreSquaresAdjacent(square1, square2);
            Assert.AreEqual(false, test);
        }

        [Test]
        public void CanDetectTwoAdjacentShips()
        {
            var testShipList = new List<IShipPosition>();
            var ship1 = new ShipPosition(new GridSquare('A',1),new GridSquare('C', 1) );
            testShipList.Add(ship1);
            var ship2 = new ShipPosition(new GridSquare('D', 1), new GridSquare('D', 5));
            var test = ShipPositionMaker.IsNotAdjacentToCurrentShip(ship2, testShipList);
            Assert.AreEqual(false,test);
        }
        [Test]
        public void WillNotDetectTwoShipsNotAdjacent()
        {
            var testShipList = new List<IShipPosition>();
            var ship1 = new ShipPosition(new GridSquare('A', 1), new GridSquare('C', 3));
            testShipList.Add(ship1);
            var ship2 = new ShipPosition(new GridSquare('F', 3), new GridSquare('F', 7));
            var test = ShipPositionMaker.IsNotAdjacentToCurrentShip(ship2, testShipList);
            Assert.AreEqual(true, test);
        }


    }
}