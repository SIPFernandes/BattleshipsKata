using BattleshipsKata.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsKata
{
    public class Board
    {
        public int MaxX { get; } = 10;
        public int MaxY { get; } = 10;
        public Dictionary<Coordinate, Ship> ShipsCoords { get; } = new();
        public Dictionary<Coordinate, bool> FiredShots { get; } = new();        
        public Carrier Carrier { get; } = new Carrier();
        public Destroyer[] Destroyers { get; } = new Destroyer[2];
        public GunShip[] GunShips { get; } = new GunShip[4];
        public List<Ship> SunkedShips = new();
    

        public Board(int? heigth = null, int? width = null) 
        {
            if (heigth.HasValue)
            {
                MaxX = heigth.Value;
            }
            
            if (width.HasValue)
            {
                MaxY = width.Value;
            }
            
            CreateShips();
        }

        public void ShowReport()
        {
            var misses = FiredShots.Values.Where(x => x == false).Count();

            var msg = new StringBuilder();

            msg.Append("[ Player1\n" +
                $"Total Shots: {FiredShots.Count}\n" +
                $"Misses: {misses}\n" +
                $"Hits: {FiredShots.Count - misses}\n" +
                $"Ships sunk: [\n");

            foreach (var ship in SunkedShips)
            {
                msg.Append($"     {ship.GetType().Name}: {ship.CellsCoords[0]},\n");
            }

            Console.WriteLine( msg.ToString());

            Console.WriteLine(ToString());
        }

        public bool FireValidCoords(Coordinate coords)
        {
            if (ShipsCoords.ContainsKey(coords) ||
                FiredShots.ContainsKey(coords))
            {
                return false;
            }            

            return true;
        }

        public override string ToString()
        {
            StringBuilder msg = new();

            msg.Append($"| ");

            for (var i = 0; i < MaxX; i++)
            {
                msg.Append($"{i} |");
            }

            msg.AppendLine();

            for (var j = 0; j < MaxY; j++)
            {
                msg.Append($"{j}");

                for (var i = 0; i < MaxX; i++)
                {
                    var coord = new Coordinate(i, j);
                    char value = '\0';

                    if (ShipsCoords.TryGetValue(coord, out var ship))
                    {                        
                        value = ship.Letter;
                    }
                    else if (FiredShots.TryGetValue(coord, out bool isHit))
                    {
                        if (isHit)
                        {
                            value = '0';
                        }
                        else
                        {
                            value = 'x';
                        }
                    }

                    msg.Append($"| {value} |");
                }

                msg.AppendLine();
            }

            return msg.ToString();
        }

        private void CreateShips()
        {
            SetShipCoords(Carrier);

            CreateAllTypeShips<Destroyer>(Destroyers);

            CreateAllTypeShips<GunShip>(GunShips);            
        }

        private void CreateAllTypeShips<T>(Ship[] ships) where T : Ship
        {
            for (var i = 0; i < ships.Length; i++)
            {                
                ships[i] = (T)Activator.CreateInstance(typeof(T))!;

                SetShipCoords(ships[i]);
            }
        }

        private void SetShipCoords(Ship ship)
        {
            for (var i = 0; i < ship.CellsCoords.Length; i++)
            {                                
                Console.WriteLine($"Set {ship.GetType().Name} cell {i + 1} coordinates: ");

                var coords = RequestUserCoords(ship, i);

                ship.CellsCoords[i] = coords;

                ShipsCoords.Add(coords, ship);
            }
        }

        private Coordinate RequestUserCoords(Ship ship, int remainingCells)
        {
            Coordinate? validBoardCoords = null;

            while (!validBoardCoords.HasValue)
            {
                var possibleCoords = Console.ReadLine();             
                
                var coords = Coordinate.ConvertStringToValidCoord(possibleCoords);     
                
                if (coords.HasValue && IsValidShipCoords(coords.Value, ship, remainingCells))
                {
                    validBoardCoords = coords;                    
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }            

            return validBoardCoords.Value;
        }

        private bool IsValidShipCoords(Coordinate coords, Ship ship, int currentCell)
        {
            if (IsInvalidCoord(coords))
            {
                return false;
            }

            if (ship.CellsCoords.Length > 1 && 
                !BigShipIsValid(coords, ship, currentCell))
            {
                return false;
            }

            return true;
        }

        private bool BigShipIsValid(Coordinate coords, Ship ship, int currentCell)
        {            
            if (currentCell == 0 && !FirstBigChipCellIsValid(ship, coords))
            {              
                return false;
            }
            else if (currentCell == 1 && !SecondBigShipCellIsValid(ship, coords, currentCell))
            {
                return false;
            }
            else if (currentCell > 1 && !OtherBigShipCellISValid(ship, coords, currentCell))
            {
                return false;
            }

            return true;
        }

        private bool OtherBigShipCellISValid(Ship ship, Coordinate coords, int currentCell)
        {
            var prevCoords = ship.CellsCoords[currentCell - 1];
            var prevPrevCoords = ship.CellsCoords[currentCell - 2];

            var xDif = coords.X - prevCoords.X;
            var yDif = coords.Y - prevCoords.Y;

            var prevXDif = prevCoords.X - prevPrevCoords.X;
            var prevYDif = prevCoords.Y - prevPrevCoords.Y;

            if (xDif == 0 && (yDif == 1 || yDif == -1))
            {
                if (prevYDif != yDif)
                {
                    return false;
                }
            }
            else if (yDif == 0 && (xDif == 1 || xDif == -1))
            {
                if (prevXDif != xDif)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }        

        private bool SecondBigShipCellIsValid(Ship ship, Coordinate coords, int currentCell)
        {
            var prevCoords = ship.CellsCoords[currentCell - 1];

            var xDif = coords.X - prevCoords.X;
            var yDif = coords.Y - prevCoords.Y;

            if (xDif == 0 && (yDif == 1 || yDif == -1))
            {
                for (var i = 1; i < ship.CellsCoords.Length - 1; i++)
                {
                    if (IsInvalidCoord(new Coordinate(coords.X + (i * yDif), coords.Y)))
                    {
                        return false;
                    }
                }
            }
            else if (yDif == 0 && (xDif == 1 || xDif == -1))
            {
                for (var i = 1; i < ship.CellsCoords.Length - 1; i++)
                {
                    if (IsInvalidCoord(new Coordinate(coords.X + (i * xDif), coords.Y)))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private bool FirstBigChipCellIsValid(Ship ship, Coordinate coords)
        {
            bool invalidNextPosCoordX = false;
            bool invalidNextPosCoordY = false;
            bool invalidNextNegCoordX = false;
            bool invalidNextNegCoordY = false;

            for (var i = 1; i < ship.CellsCoords.Length; i++)
            {
                if (!invalidNextPosCoordX)
                {
                    invalidNextPosCoordX = IsInvalidCoord(new Coordinate(coords.X + i, coords.Y));
                }

                if (!invalidNextPosCoordY)
                {
                    invalidNextPosCoordY = IsInvalidCoord(new Coordinate(coords.X, coords.Y + i));
                }

                if (!invalidNextNegCoordX)
                {
                    invalidNextNegCoordX = IsInvalidCoord(new Coordinate(coords.X - i, coords.Y));
                }

                if (!invalidNextNegCoordY)
                {
                    invalidNextNegCoordY = IsInvalidCoord(new Coordinate(coords.X, coords.Y - i));
                }

                if (invalidNextPosCoordX && invalidNextPosCoordY && invalidNextNegCoordX && invalidNextNegCoordY)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsInvalidCoord(Coordinate coord)
        {
            if (!IsValidBoardCoords(coord) || !IsValidNoConflictCoords(coord))
            {
                return true;
            }

            return false;
        }

        private bool IsValidNoConflictCoords(Coordinate coords)
        {
            if (ShipsCoords.ContainsKey(coords))
            {
                return false;
            }           

            return true;
        }

        private bool IsValidBoardCoords(Coordinate coords)
        {           
            if (coords.X > MaxX || coords.X < 0)
            {
                return false;
            }

            if (coords.Y > MaxY || coords.Y < 0)
            {
                return false;
            }

            return true;
        }
    }
}
