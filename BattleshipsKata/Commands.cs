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
            players[0].Board = new Board();
            players[1].Board = new Board();
        }

        public void EndTurn() { throw new NotImplementedException();}
        public static void Print(Player player) 
        {
            Console.Write($"| ");

            for (var i = 0; i < player.Board!.MaxX; i++)
            {
                Console.Write($"{i} |");
            }            

            Console.WriteLine();

            for (var j = 0; j < player.Board!.MaxY; j++)
            {
                Console.Write($"{j}");

                for (var i = 0; i < player.Board!.MaxX; i++)
                {
                    var coord = new Coordinate(i, j);

                    if (player.Board.ShipsCoords.ContainsKey(coord))
                    {
                        var ship = player.Board.ShipsCoords[coord];

                        Console.Write($"| { ship.Letter } |");
                    }
                    else
                    {
                        Console.Write($"|   |");
                    }
                }

                Console.WriteLine();
            }            
        }
        public void Fire() { throw new NotImplementedException();}
    }
}
