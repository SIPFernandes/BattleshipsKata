namespace BattleshipsKata.Services.Interfaces
{
    public class PlayerService : IPlayerService
    {
        public Player CreatePlayer()
        {
            var playerName = string.Empty;

            while (string.IsNullOrEmpty(playerName))
                playerName = Console.ReadLine();

            return new Player(playerName);
        }
    }
}