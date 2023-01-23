using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsKata.Services.Interfaces
{
    public interface IBoardServices
    {
        public Board CreatNewBoard();
        public string PrintBoard(Board board);
    }
}
