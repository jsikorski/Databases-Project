using System;
using Autofac;
using Caliburn.Micro;
using Client.Commands.Reservations;
using Client.Messages;
using Common.Commands;
using Common.Infrastucture;
using Common.Messages;
using Connection;
using System.Linq;

namespace Client.Features.Reservations
{
    public class ReservationsViewModel : Screen, IBusyScopeSubscreen, 
        IHandle<ReservationsFounded>, IHandle<ReservationRemoved>, IHandle<ReservationPaid>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Func<RESERVATION, RemoveReservation> _removeReservationFactory;
        private readonly Func<RESERVATION, PayForReservation> _payForReservationFactory;
        private readonly IContainer _container;
        private IBusyScope _busyScope;

        public BindableCollection<ReservationViewModel> Reservations { get; private set; }
        private ReservationViewModel _selectedReservation;
        public ReservationViewModel SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                _selectedReservation = value;
                NotifyOfPropertyChange(() => CanPayForReservation);
                NotifyOfPropertyChange(() => CanRemoveReservation);
            }
        }

        public bool CanPayForReservation
        {
            get { return SelectedReservation != null && !SelectedReservation.IsPaid; }
        }

        public bool CanRemoveReservation
        {
            get { return SelectedReservation != null; }
        }

        public ReservationsViewModel(
            IEventAggregator eventAggregator,
            Func<RESERVATION, RemoveReservation> removeReservationFactory, 
            Func<RESERVATION, PayForReservation> payForReservationFactory,
            IContainer container)
        {
            _eventAggregator = eventAggregator;
            _removeReservationFactory = removeReservationFactory;
            _payForReservationFactory = payForReservationFactory;
            _container = container;

            Reservations = new BindableCollection<ReservationViewModel>();
        }

        public void ReloadReservations()
        {
            _eventAggregator.Subscribe(this);
            var command = _container.Resolve<GetReservationsForUser>();
            CommandInvoker.InvokeBusy(command, _busyScope);
        }

        public void PayForReservation()
        {
            _eventAggregator.Subscribe(this);
            ICommand command = _payForReservationFactory(SelectedReservation.Reservation);
            CommandInvoker.InvokeBusy(command, _busyScope);
        }

        public void RemoveReservation()
        {
            _eventAggregator.Subscribe(this);
            ICommand command = _removeReservationFactory(SelectedReservation.Reservation);
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
            Reservations.AddRange(message.Reservations.Select(reservation => new ReservationViewModel(reservation)));
        }

        public void Handle(ReservationRemoved message)
        {
            _eventAggregator.Unsubscribe(this);
            Reservations.Remove(SelectedReservation);
        }

        public void Handle(ReservationPaid message)
        {
            _eventAggregator.Unsubscribe(this);
            ReservationViewModel paidReservation =
                Reservations.Single(reservation => reservation.Reservation == message.Reservation);
            paidReservation.IsPaid = true;
        }
    }
}
