namespace BattleshipsKata
{
    public class Game
    {
        public Commands Commands { get; }
        public Player[] Players { get; }        

        public Game() 
        { 
            Commands = new Commands();
            Players = new Player[2];            
        }
    }
}
