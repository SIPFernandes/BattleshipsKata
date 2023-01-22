using BattleshipsKata.Ships;

namespace BattleshipsKata
{
    public class Commands
    {       
        public Commands() 
        { 

        }

        public static Player AddPlayer()
        {
            var playerName = string.Empty;

            while (string.IsNullOrEmpty(playerName))
                playerName = Console.ReadLine();

            return new Player(playerName);
        }

        public static void Start(Player[] players)
        {
            //Must use a board Service to provide the boards in order to mock it
            players[0].Board = new Board();
            players[1].Board = new Board();
        }

        public void EndTurn() { throw new NotImplementedException();}
        public static void Print(Player player) 
        {
            Console.WriteLine(player.Board!.ToString());
        }

        public void Fire(Player playerTurn, Player enemyPlayer) 
        {            
            Coordinate? validCoords = null;

            while (!validCoords.HasValue)
            {
                var possibleCoords = Console.ReadLine();

                var coords = Coordinate.ConvertStringToValidCoord(possibleCoords);

                if (coords.HasValue && playerTurn.Board!.FireValidCoords(coords.Value))
                {
                    validCoords = coords;
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }

            if (enemyPlayer.Board!.ShipsCoords.TryGetValue(validCoords.Value, out Ship? ship))
            {
                playerTurn.Board!.FiredShots.Add(validCoords.Value, true);

                var destroyedShipCells = 0;

                foreach (var cell in ship.CellsCoords)
                {
                    if (playerTurn.Board!.FiredShots.ContainsKey(cell))
                    {          
                        destroyedShipCells++;
                    }
                }

                if (destroyedShipCells == ship.CellsCoords.Length)
                {
                    playerTurn.Board.SunkedShips.Add(ship);

                    Console.WriteLine("Ship has sunk");
                }

                if (playerTurn.Board.SunkedShips.Count == 7)
                {
                    playerTurn.Board.ShowReport();                    
                    enemyPlayer.Board.ShowReport();                    
                }
            }
            else
            {
                playerTurn.Board!.FiredShots.Add(validCoords.Value, false);
            }
        }
    }
}
