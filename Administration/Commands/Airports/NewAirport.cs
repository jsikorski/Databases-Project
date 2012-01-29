using Administration.Features.Airports;
using Caliburn.Micro;
using Common.Infrastucture;

namespace Administration.Commands.Airports
{
    public class NewAirport : ICommand
    {
        private readonly IWindowManager _windowManager;
        private readonly NewAirportViewModel _newAirport;

        public NewAirport(
            IWindowManager windowManager,
            NewAirportViewModel newAirport)
        {
            _windowManager = windowManager;
            _newAirport = newAirport;
        }

        public void Execute()
        {
            _windowManager.ShowDialog(_newAirport);
        }
    }
}
