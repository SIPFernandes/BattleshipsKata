using BattleshipsKata.Services.Interfaces;
using Moq;

namespace BattleshipsKata.Tests.UnitTests
{
    public class CommandsShould
    {
        private readonly Commands _commands;        
        private readonly Mock<IBoardServices> _boardServices;        

        public CommandsShould()
        {
            _boardServices = new Mock<IBoardServices>();            

            _commands = new Commands(_boardServices.Object);            
        }

        [Fact]
        public void AddNewPlayer()
        {            
            var playerName = "Player1";
            Player? player = null;

            using (var sr = new StringReader(playerName))
            {
                Console.SetIn(sr);

                player = _commands.AddPlayer();                              
            }            

            Assert.NotNull(player);
            Assert.True(player.Name == playerName);
        }

        [Fact]
        public void StartGame()
        {
            var player1 = new Player("Player1");
            var player2 = new Player("Player2");                       

            _commands.Start(new Player[]
            {
                  player1,
                  player2
            });

            _boardServices.Verify(x => x.CreatNewBoard(), Times.Exactly(2));
        }

        [Fact]
        public void Print()
        {
            var player1 = new Player("Player1");

            var boardPrint = "Print Called";

            _boardServices.Setup(x => x.PrintBoard(player1.Board!))
                .Returns(boardPrint);

            var sr = new StringWriter();

            Console.SetOut(sr);

            _commands.Print(player1);

            Assert.Equal(boardPrint + "\r\n", sr.ToString());
        }
    }
}
