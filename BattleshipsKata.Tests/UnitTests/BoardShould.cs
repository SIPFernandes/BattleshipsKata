using BattleshipsKata.Models.Ships;
using Microsoft.VisualStudio.CodeCoverage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsKata.Tests.UnitTests
{
    public class BoardShould
    {

        private const string CarrierCoords = "0,0\n0,1\n0,2\n0,3\n";
        private const string DestroyerCoords1 = "1,0\n1,1\n1,2\n";
        private const string DestroyerCoords2 = "2,0\n2,1\n2,2\n";
        private const string GunShip1 = "3,0\n";
        private const string GunShip2 = "4,0\n";
        private const string GunShip3 = "5,0\n";
        private const string GunShip4 = "6,0\n";
        private readonly Board _board;
        public BoardShould() 
        {            
            var input = CarrierCoords + DestroyerCoords1 + DestroyerCoords2 +
                GunShip1 + GunShip2 + GunShip3 + GunShip4;

            using var sr = new StringReader(input);

            Console.SetIn(sr);

            _board = new Board();            
        }

        [Fact]
        public void DefaultBoardSizeIs10()
        {
            Assert.Equal(10, _board.MaxY);
            Assert.Equal(10, _board.MaxX);
        }

        [Fact]
        public void CreateAllUserShipsOnStart()
        {
            Assert.NotNull(_board.Carrier);

            CheckShipsAreNotNull(_board.Destroyers);
            CheckShipsAreNotNull(_board.GunShips);
        }

        [Fact]
        public void SetAllUserShipCoords()
        {
            CheckShipCoords(CarrierCoords, _board.Carrier);
            CheckShipCoords(DestroyerCoords1, _board.Destroyers[0]);
            CheckShipCoords(DestroyerCoords2, _board.Destroyers[1]);            
            CheckShipCoords(GunShip1, _board.GunShips[0]);            
            CheckShipCoords(GunShip2, _board.GunShips[1]);            
            CheckShipCoords(GunShip3, _board.GunShips[2]);            
            CheckShipCoords(GunShip4, _board.GunShips[3]);            
        }

        [Fact]
        public void InvalidCoordinateMessage()
        {
            var carrierCoords = "0,0\n0,-1\n0,1\n0,2\n0,3\n";
            var destroyerCoords1 = "1,0\n1,1\n2,1\n1,2\n";
            var destroyerCoords2 = "0,0\n2,0\n2,1\n2,2\n";
            var gunShip1 = "3,0\n";
            string gunShip2 = "4,0\n";
            string gunShip3 = "5,0\n";
            string gunShip4 = "6,0\n";

            var input = carrierCoords + destroyerCoords1 + destroyerCoords2 +
                gunShip1 + gunShip2 + gunShip3 + gunShip4;

            var sw = new StringWriter();
            using var sr = new StringReader(input);

            Console.SetOut(sw);
            Console.SetIn(sr);

            var board = new Board();
            var result = sw.ToString();

            var expectedResult = $"Set {nameof(Carrier)} cell {1} coordinates: \r\n" +
                $"Set {nameof(Carrier)} cell {2} coordinates: \r\n" +
                "Invalid Input\r\n" +
                $"Set {nameof(Carrier)} cell {3} coordinates: \r\n" +
                $"Set {nameof(Carrier)} cell {4} coordinates: \r\n" +
                $"Set {nameof(Destroyer)} cell {1} coordinates: \r\n" +
                $"Set {nameof(Destroyer)} cell {2} coordinates: \r\n" +
                $"Set {nameof(Destroyer)} cell {3} coordinates: \r\n" +
                "Invalid Input\r\n" +
                $"Set {nameof(Destroyer)} cell {1} coordinates: \r\n" +
                "Invalid Input\r\n" +
                $"Set {nameof(Destroyer)} cell {2} coordinates: \r\n" +
                $"Set {nameof(Destroyer)} cell {3} coordinates: \r\n" +
                $"Set {nameof(GunShip)} cell {1} coordinates: \r\n" +
                $"Set {nameof(GunShip)} cell {1} coordinates: \r\n" +
                $"Set {nameof(GunShip)} cell {1} coordinates: \r\n" +
                $"Set {nameof(GunShip)} cell {1} coordinates: \r\n";

            Assert.Equal(expectedResult, result);                      
        }

        private void CheckShipCoords(string coords, Ship ship)
        {
            var coordsArray = coords.Split("\n");

            foreach (var stringCoord in coordsArray.SkipLast(1))
            {
                var array = stringCoord.Split(',');                

                var coord = new Coordinate(int.Parse(array[0]), int.Parse(array[1]));

                Assert.True(_board.ShipsCoords[coord] == ship.Id);
            }
        }

        private static void CheckShipsAreNotNull(Ship[] ships)
        {
            foreach (var ship in ships)
            {
                Assert.NotNull(ship);
            }
        }
    }
}
