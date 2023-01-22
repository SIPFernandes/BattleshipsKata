namespace BattleshipsKata.Ships
{
    public abstract class Ship
    {
        public string Id { get; }
        public virtual char Letter { get; }
        public virtual Coordinate[] CellsCoords { get; } = new Coordinate[1];

        public Ship()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
