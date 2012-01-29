using System;
using Administration.Commands;
using Administration.Commands.Reservations;
using Administration.Messages;
using Caliburn.Micro;
using Common.Infrastucture;
using Connection;

namespace Administration.Features.Reservations
{
    public class ReservationsViewModel : Screen, IBusyScopeSubscreen, IHandle<ReservationsFounded>, IHandle<ReservationRemoved>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Func<ReservationsSearchData, SearchReservations> _searchReservationsFactory;
        private readonly Func<RESERVATION, RemoveReservation> _removeReservationFactory;
        private IBusyScope _busyScope;

        public BindableCollection<RESERVATION> Reservations { get; private set; }
        private RESERVATION _selectedReservation;
        public RESERVATION SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                _selectedReservation = value;
                NotifyOfPropertyChange(() => CanRemoveReservation);
            }
        }

        public ReservationsSearchData ReservationsSearchData { get; set; }

        public bool CanRemoveReservation
        {
            get { return SelectedReservation != null; }
        }

        public ReservationsViewModel(
            IEventAggregator eventAggregator,
            Func<ReservationsSearchData, SearchReservations> searchReservationsFactory,
            Func<RESERVATION, RemoveReservation> removeReservationFactory)
        {
            _eventAggregator = eventAggregator;
            _searchReservationsFactory = searchReservationsFactory;
            _removeReservationFactory = removeReservationFactory;

            Reservations = new BindableCollection<RESERVATION>();
        }

        public void SearchReservations()
        {
            _eventAggregator.Subscribe(this);
            ICommand command = _searchReservationsFactory(ReservationsSearchData);
            CommandInvoker.InvokeBusy(command, _busyScope);
        }

        public void RemoveReservation()
        {
            _eventAggregator.Subscribe(this);
            ICommand command = _removeReservationFactory(SelectedReservation);
            CommandInvoker.Execute(command);
        }

        public void SetBusyScope(IBusyScope busyScope)
        {
            _busyScope = busyScope;
        }

        public void Handle(ReservationsFounded message)
        {
            _eventAggregator.Unsubscribe(this);
            Reservations.Clear();
            Reservations.AddRange(message.Reservations);
        }

        public void Handle(ReservationRemoved message)
        {
            _eventAggregator.Unsubscribe(this);
            Reservations.Remove(SelectedReservation);
        }
    }
}
