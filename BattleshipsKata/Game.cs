using BattleshipsKata.Services.Implementations;

namespace BattleshipsKata
{
    public class Game
    {
        public Commands Commands { get; }
        public Player[] Players { get; }        

        public Game() 
        {
            var boardService = new BoardServices();
            Commands = new Commands(boardService);
            Players = new Player[2];            
        }
    }
}
