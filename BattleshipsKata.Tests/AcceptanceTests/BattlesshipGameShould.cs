using BattleshipsKata.Ships;
using System.Numerics;

namespace BattleshipsKata.Tests.AcceptanceTests
{
    public class BattlesshipGameShould
    {
        [Fact]
        public void DisplayBattleReport()
        {
            var game = new Game();

            var player1Name = "Player1";
            var player2Name = "Player2";

            using (var sr = new StringReader(player1Name))
            {
                Console.SetIn(sr);

                game.Players[0] = game.Commands.AddPlayer();
            }

            using (var sr = new StringReader(player2Name))
            {
                Console.SetIn(sr);

                game.Players[1] = game.Commands.AddPlayer();
            }

            //var player1StartCoordinates = new Dictionary<string, Coords>()

            game.Commands.Start(game.Players);            
        }
    }
}