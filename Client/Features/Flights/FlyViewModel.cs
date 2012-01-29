using Connection;

namespace Client.Features.Flights
{
    public class FlyViewModel
    {
        public FLY Fly { get; private set; }

        public string FromCityName
        {
            get { return Fly.CONNECTION.AIRPORT.CITY.NAME; }
        }

        public string ToCityName
        {
            get { return Fly.CONNECTION.AIRPORT1.CITY.NAME; }
        }

        public string Date
        {
            get { return Fly.FLY_DATE.ToShortDateString(); }
        }

        public string NumberOfPlaces
        {
            get { return string.Format("{0} / {1}", Fly.FREE_PLACES_NUMBER, Fly.CONNECTION.TICKETS); }
        }

        public decimal Price
        {
            get { return Fly.CONNECTION.PRICE; }
        }

        public FlyViewModel(FLY fly)
        {
            Fly = fly;
        }
    }
}
