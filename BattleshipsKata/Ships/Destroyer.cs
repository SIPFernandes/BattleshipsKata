namespace BattleshipsKata.Ships
{
    public class Destroyer : Ship
    {
        public override char Letter { get; }
        public override Coordinate[] CellsCoords { get; }
        public Destroyer()
        {
            Letter = 'd';
            CellsCoords = new Coordinate[3];
        }
    }
}
