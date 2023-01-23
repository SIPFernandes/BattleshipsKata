using BattleshipsKata.Services.Interfaces;

namespace BattleshipsKata.Services.Implementations
{
    public class BoardServices : IBoardServices
    {
        public Board CreatNewBoard()
        {
            return new Board();
        }

        public string PrintBoard(Board board)
        {
            return board.ToString();
        }
    }
}
