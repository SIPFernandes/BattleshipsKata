namespace BattleshipsKata.Ships
{
    public class Carrier : Ship
    {
        public override Coordinate[] CellsCoords { get; }
        public Carrier()
        {
            CellsCoords = new Coordinate[4];
        }
    }
}
