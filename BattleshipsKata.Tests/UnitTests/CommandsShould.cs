using BattleshipsKata.Ships;
using Moq;
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
            var player1 = new Player("Player1");
            var player2 = new Player("Player2");
            
            Commands.Start(new Player[]
            {
                  player1,
                  player2
            });

            Assert.NotNull(player1.Board);            
            Assert.NotNull(player2.Board);            
        }

        [Fact]
        public void Print()
        {
            var player1 = new Player("player1");

            var boardMock = new Mock<Board>();

            var carrier = new Carrier();
            var destroyer = new Destroyer();
            var gunShip1 = new GunShip();
            var gunShip2 = new GunShip();

            boardMock.SetupGet(x => x.Destroyers)
                .Returns(new Destroyer[]
                {
                    destroyer
                });                
            boardMock.SetupGet(x => x.GunShips)
                .Returns(new GunShip[]
                {
                    gunShip1
                });

            boardMock.SetupGet(x => x.ShipsCoords)
                .Returns(() => new()
                {
                    { new Coordinate(0, 0), carrier },
                    { new Coordinate(0, 1), carrier },
                    { new Coordinate(0, 2), carrier },
                    { new Coordinate(0, 3), carrier },
                    { new Coordinate(1, 1), destroyer },
                    { new Coordinate(1, 2), destroyer },
                    { new Coordinate(1, 3), destroyer },
                    { new Coordinate(4, 4), gunShip1 },
                    { new Coordinate(5, 5), gunShip2 },
                });

            player1.Board = boardMock.Object;

            using (var sw = new StringWriter())
            {
                Commands.Print(player1);                

                Console.WriteLine(sw.ToString());
            }
        }
    }
}
