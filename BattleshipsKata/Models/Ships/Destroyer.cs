namespace BattleshipsKata.Models.Ships
{
    public class Destroyer : Ship
    {
        public override Coordinate[] CellsCoords { get; }
        public Destroyer()
        {
            CellsCoords = new Coordinate[3];
        }
    }
}
