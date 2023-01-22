namespace BattleshipsKata.Ships
{
    public class GunShip : Ship
    {
        public override char Letter { get; }

        public GunShip() 
        {
            Letter = 'g';
        }
    }
}
