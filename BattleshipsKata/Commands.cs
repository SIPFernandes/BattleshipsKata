using BattleshipsKata.Services.Interfaces;

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
        public void Print() { throw new NotImplementedException();}
        public void Fire() { throw new NotImplementedException();}
    }
}
