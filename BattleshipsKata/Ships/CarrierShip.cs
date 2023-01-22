namespace BattleshipsKata.Ships
{
    public class Carrier : Ship
    {
        public override char Letter { get; }
        public override Coordinate[] CellsCoords { get; }
        public Carrier()
        {
            Letter = 'c';
            CellsCoords = new Coordinate[4];
        }
    }
}
