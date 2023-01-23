using BattleshipsKata.Services.Interfaces;
using BattleshipsKata.Ships;

namespace BattleshipsKata
{
    public class Commands
    {
        private readonly IBoardServices _boardServices;
        public Commands(IBoardServices boardServices) 
        {
            _boardServices = boardServices;
        }

        public Player AddPlayer()
        {
            var playerName = string.Empty;

            while (string.IsNullOrEmpty(playerName))
                playerName = Console.ReadLine();

            return new Player(playerName);
        }

        public void Start(Player[] players)
        {            
            players[0].Board = _boardServices.CreatNewBoard();
            players[1].Board = _boardServices.CreatNewBoard();
        }

        public void EndTurn() { throw new NotImplementedException();}
        public void Print(Player player) 
        {
            var result = _boardServices.PrintBoard(player.Board!);

            Console.WriteLine(result);
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
