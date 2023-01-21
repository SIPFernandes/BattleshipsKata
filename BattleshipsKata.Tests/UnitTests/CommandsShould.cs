using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsKata.Tests.UnitTests
{
    public class CommandsShould
    {
        private readonly Commands _commands;

        public CommandsShould()
        {
            _commands = new Commands();
        }

        [Fact]
        public void AddNewPlayer()
        {            
            var playerName = "Player1";
            Player? player = null;

            using (var sr = new StringReader(playerName))
            {
                Console.SetIn(sr);

                player = Commands.AddPlayer();                              
            }            

            Assert.NotNull(player);
            Assert.True(player.Name == playerName);
        }

        [Fact]
        public void StartGame()
        {
            var player1Name = "Player1";
            Player? player1 = null;

            using (var sr = new StringReader(player1Name))
            {
                Console.SetIn(sr);

                player1 = Commands.AddPlayer();
            }

            var player2Name = "Player2";
            Player? player2 = null;

            using (var sr = new StringReader(player2Name))
            {
                Console.SetIn(sr);

                player2 = Commands.AddPlayer();
            }

            Commands.Start(new Player[]
            {
                player1,
                player2
            });

            Assert.NotNull(player1.Board);            
            Assert.NotNull(player2.Board);            
        }
    }
}
