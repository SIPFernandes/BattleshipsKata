using BattleshipsKata.Services.Interfaces;

namespace BattleshipsKata
{
    public class Commands
    {       
        public Commands(PlayerService service) 
        { 

        }

        public static Player AddPlayer()
        {
            
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
